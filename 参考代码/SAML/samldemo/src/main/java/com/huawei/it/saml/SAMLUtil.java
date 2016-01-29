package com.huawei.it.saml;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.Serializable;
import java.io.StringWriter;
import java.security.InvalidAlgorithmParameterException;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.Provider;
import java.security.KeyStore.PrivateKeyEntry;
import java.security.cert.X509Certificate;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;

import javax.xml.crypto.MarshalException;
import javax.xml.crypto.dsig.CanonicalizationMethod;
import javax.xml.crypto.dsig.DigestMethod;
import javax.xml.crypto.dsig.Reference;
import javax.xml.crypto.dsig.SignatureMethod;
import javax.xml.crypto.dsig.SignedInfo;
import javax.xml.crypto.dsig.Transform;
import javax.xml.crypto.dsig.TransformService;
import javax.xml.crypto.dsig.XMLSignature;
import javax.xml.crypto.dsig.XMLSignatureException;
import javax.xml.crypto.dsig.XMLSignatureFactory;
import javax.xml.crypto.dsig.dom.DOMSignContext;
import javax.xml.crypto.dsig.keyinfo.KeyInfo;
import javax.xml.crypto.dsig.keyinfo.KeyInfoFactory;
import javax.xml.crypto.dsig.keyinfo.X509Data;
import javax.xml.crypto.dsig.spec.C14NMethodParameterSpec;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.apache.jcp.xml.dsig.internal.dom.DOMTransform;
import org.jdom.Element;
import org.jdom.Namespace;
import org.jdom.input.DOMBuilder;
import org.jdom.output.XMLOutputter;
import org.w3c.dom.Node;
import org.xml.sax.SAXException;

public class SAMLUtil {
	public static String toStanderTime(Date date) {
		SimpleDateFormat dayFormat = new SimpleDateFormat("yyyy-MM-dd");
		SimpleDateFormat timeFormat = new SimpleDateFormat("HH:mm:ss");

		TimeZone zone = TimeZone.getTimeZone("GMT");
		dayFormat.setTimeZone(zone);
		timeFormat.setTimeZone(zone);
		return dayFormat.format(date) + 'T' + timeFormat.format(date) + 'Z';
	}
	
	public static Calendar toDate(String t) {
		String date = t.substring(0, t.indexOf("T"));
		String time = t.substring(t.indexOf("T") + 1, t.length() - 1);
		SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
		SimpleDateFormat timeFormat = new SimpleDateFormat("HH:mm:ss");
		// Time in GMT
		TimeZone zone = TimeZone.getTimeZone("GMT");
		dateFormat.setTimeZone(zone);
		timeFormat.setTimeZone(zone);
		try {
			Date dd = dateFormat.parse(date);
			Date tt = timeFormat.parse(time);
			long m = dd.getTime() + tt.getTime();
			Calendar cal = Calendar.getInstance();
			cal.setTimeInMillis(m);
			return cal;
		} catch (ParseException e) {
			throw new IllegalArgumentException("Invalid SMAL time");
		}
	}

	public static void addNamespace(Element root, Namespace namespace) {
		if (root == null) {
			return;
		}
		root.setNamespace(namespace);
		if (root.getChildren() == null) {
			return;
		}
		for (int index = 0; index < root.getChildren().size(); index++) {
			Element child = (Element) root.getChildren().get(index);
			addNamespace(child, namespace);
		}
	}

	public static void insertSignature(org.w3c.dom.Element element,
			PrivateKeyEntry privateKey, Node insertBefore) throws SAMLException {
		String JSR_105_PROVIDER = "org.apache.jcp.xml.dsig.internal.dom.XMLDSigRI";
		String providerName = System.getProperty("jsr105Provider",
				JSR_105_PROVIDER);
		try {
			Provider provider = (Provider) Class.forName(providerName).newInstance();
			XMLSignatureFactory factory = XMLSignatureFactory.getInstance("DOM", provider);
			TransformService ts = TransformService.getInstance(Transform.ENVELOPED, "DOM", provider);
			DOMTransform domTrasform = new DOMTransform(ts);
			CanonicalizationMethod canMethod = factory.newCanonicalizationMethod(
							CanonicalizationMethod.EXCLUSIVE, (C14NMethodParameterSpec) null);
			SignatureMethod signatureMethod = factory.newSignatureMethod(
					SignatureMethod.RSA_SHA1, null);
			element.setIdAttribute("ID", true);
			String refId = element.getAttribute("ID");
			Reference ref = factory.newReference("#" + refId, 
					factory.newDigestMethod(DigestMethod.SHA1, null),
					Collections.singletonList(domTrasform), null, null);
			SignedInfo signedInfo = factory.newSignedInfo(canMethod,
					signatureMethod, Collections.singletonList(ref));
			X509Certificate cert = (X509Certificate) privateKey.getCertificate();
			KeyInfoFactory kif = factory.getKeyInfoFactory();
			List<Serializable> x509Content = new ArrayList<Serializable>();
			x509Content.add(cert);
			X509Data xd = kif.newX509Data(x509Content);
			KeyInfo keyInfo = kif.newKeyInfo(Collections.singletonList(xd));
			PrivateKey key = privateKey.getPrivateKey();
			DOMSignContext dsc = new DOMSignContext(key, element);
			dsc.setNextSibling(insertBefore);
			XMLSignature signature = factory.newXMLSignature(signedInfo,keyInfo);
			signature.sign(dsc);
		} catch (IllegalAccessException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		} catch (MarshalException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		} catch (XMLSignatureException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		} catch (InstantiationException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		} catch (ClassNotFoundException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		} catch (NoSuchAlgorithmException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		} catch (InvalidAlgorithmParameterException e) {
			throw new SAMLException("Error insertSignature:" + e.getMessage(),e);
		}
	}

	public static org.w3c.dom.Document toDom(org.jdom.Document doc)
			throws SAMLException {
		try {
			XMLOutputter xmlOutputter = new XMLOutputter();
			StringWriter elemStrWriter = new StringWriter();
			xmlOutputter.output(doc, elemStrWriter);
			byte[] xmlBytes = elemStrWriter.toString().getBytes();
			DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
			dbf.setNamespaceAware(true);
			return dbf.newDocumentBuilder().parse(
					new ByteArrayInputStream(xmlBytes));
		} catch (IOException e) {
			throw new SAMLException("Error JDOM to W3 DOM: " + e.getMessage(), e);
		} catch (ParserConfigurationException e) {
			throw new SAMLException("Error JDOM to W3 DOM: " + e.getMessage(), e);
		} catch (SAXException e) {
			throw new SAMLException("Error JDOM to W3 DOM: " + e.getMessage(), e);
		}
	}

	public static org.w3c.dom.Element toDom(org.jdom.Element element)
			throws SAMLException {
		org.jdom.Document doc = element.getDocument();
		if (doc == null) {
			throw new SAMLException("Invalid JDOM element");
		}
		return toDom(element.getDocument()).getDocumentElement();
	}

	public static org.jdom.Element toJdom(org.w3c.dom.Element e) {
		DOMBuilder builder = new DOMBuilder();
		org.jdom.Element jdomElem = builder.build(e);
		return jdomElem;
	}
}
