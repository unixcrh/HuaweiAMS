package com.huawei.it.saml.impl;

import java.security.Provider;
import java.security.PublicKey;
import java.security.cert.CertificateExpiredException;
import java.security.cert.CertificateNotYetValidException;
import java.security.cert.X509Certificate;
import java.util.Calendar;
import java.util.Date;

import javax.xml.crypto.MarshalException;
import javax.xml.crypto.dsig.XMLSignature;
import javax.xml.crypto.dsig.XMLSignatureException;
import javax.xml.crypto.dsig.XMLSignatureFactory;
import javax.xml.crypto.dsig.dom.DOMValidateContext;

import org.w3c.dom.Node;

import com.huawei.it.saml.SAMLException;
import com.huawei.it.saml.SAMLResponse;
import com.huawei.it.saml.SAMLResponseValidator;
import com.huawei.it.saml.SAMLUtil;
import com.huawei.it.saml.ServiceProvider;

public class SAMLResponseValidatorImpl implements  SAMLResponseValidator {
	private static final String JSR_105_PROVIDER = "org.apache.jcp.xml.dsig.internal.dom.XMLDSigRI";
	
	private SAMLResponse response;
	private ServiceProvider provider;
	public SAMLResponseValidatorImpl(String response) throws SAMLException {
		this.response = new SAMLResponseImpl(response);
		this.provider = new ServiceProviderImpl();
	}

	public void validate() throws SAMLException{
		if (!validServiceProvider()) {
			throw new SAMLException("UnknownServiceProvider");
		}
		if (timeNoReache()) {
			throw new SAMLException("TimeNoReache");
		}
		if (expire()) {
			throw new SAMLException("Expire");
		}
		if (!validCertificate()) {
			throw new SAMLException("InValidCertificate");
		}
		if (!isValidXMLSign()) {
			throw new SAMLException("InValidSignature");
		}
	}

	public String getUserId() {
		return response.getNameId();
	}

	private boolean validServiceProvider() {
		return response.getIssuer().equalsIgnoreCase("www.huawei.com");
	}

	private boolean timeNoReache() {
		Calendar now = Calendar.getInstance();
		return SAMLUtil.toDate(response.getNotBefore()).after(now);
	}

	private boolean expire() {
		Calendar now = Calendar.getInstance();
		return SAMLUtil.toDate(response.getNotOnOrAfter()).before(now);
	}
	
	private boolean validCertificate() throws SAMLException {
		try {
			((X509Certificate) provider.getCertificate())
					.checkValidity(new Date());
		} catch (CertificateExpiredException e) {
			throw new SAMLException(e);
		} catch (CertificateNotYetValidException e) {
			throw new SAMLException(e);
		}
		return true;
	}



	public boolean isValidXMLSign() throws SAMLException {
		boolean coreValidity = false;
		Node signatreuNode = response.getSignature();
		String providerName = System.getProperty("jsr105Provider", JSR_105_PROVIDER);
		XMLSignatureFactory fac;
		try {
			fac = XMLSignatureFactory.getInstance("DOM",
					(Provider) Class.forName(providerName).newInstance());
		} catch (IllegalAccessException e) {
			throw new SAMLException("Cannot instance XMLSignatureFactory");
		} catch (InstantiationException e) {
			throw new SAMLException("Cannot instance XMLSignatureFactory");
		} catch (ClassNotFoundException e) {
			throw new SAMLException("Cannot instance XMLSignatureFactory");
		}
		X509Certificate x509Certificate = (X509Certificate) provider.getCertificate();
		PublicKey publicKey = x509Certificate.getPublicKey();
		DOMValidateContext valContext = new DOMValidateContext(publicKey,signatreuNode);
		try {
			XMLSignature signature = fac.unmarshalXMLSignature(valContext);
			coreValidity = signature.validate(valContext);
		} catch (MarshalException e) {
			throw new SAMLException("Cannot unmarshalXMLSignature:" + e.getMessage(),e);
		} catch (XMLSignatureException e) {
			throw new SAMLException("XMLSignatureException:" + e.getMessage(),e);
		}
		return coreValidity;
	}

	@Override
	public String getNameId() {
		return response.getNameId();
	}
}
