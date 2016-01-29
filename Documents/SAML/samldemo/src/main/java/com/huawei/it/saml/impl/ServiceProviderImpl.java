package com.huawei.it.saml.impl;

import java.io.IOException;
import java.io.InputStream;
import java.security.KeyStore.PrivateKeyEntry;
import java.security.PublicKey;
import java.security.cert.Certificate;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.util.Properties;

import com.huawei.it.keystore.KeyInfoException;
import com.huawei.it.keystore.KeystoreInfo;
import com.huawei.it.saml.ServiceProvider;
import com.huawei.it.security.Encrypt;
import com.huawei.it.security.EncryptException;

public class ServiceProviderImpl implements ServiceProvider {
	private static String issuer;
	private static String serviceUrl;
	private static KeystoreInfo keyInfo;
	private static Certificate certificate;
	static {
		ClassLoader cl = Thread.currentThread().getContextClassLoader();
		InputStream is = null;
		InputStream jks = null;
		InputStream cer = null;
		
		Properties properties = new Properties();
		try {
			is = cl.getResourceAsStream("config/saasclient.properties");
			properties.load(is);
			issuer = properties.getProperty("issuer");
			serviceUrl = properties.getProperty("serviceurl");
			String store = properties.getProperty("keystore");
			jks = cl.getResourceAsStream(store);
			String spass = Encrypt.decryptWithDES(properties.getProperty("storepassword"));
			String kpass = Encrypt.decryptWithDES(properties.getProperty("keypassword"));
			String alias = properties.getProperty("keyalias");
			keyInfo = new KeystoreInfo(jks, spass.toCharArray(), kpass.toCharArray(), alias);
			CertificateFactory caf = CertificateFactory.getInstance("X.509");
			cer = cl.getResourceAsStream(properties.getProperty("certificatepath"));
			certificate = caf.generateCertificate(cer);
		} catch (IOException e) {
			throw new RuntimeException(e);
		} catch (EncryptException e) {
			throw new RuntimeException(e);
		} catch (KeyInfoException e) {
			throw new RuntimeException(e);
		} catch (CertificateException e) {
			throw new RuntimeException(e);
		} finally {
			if (is != null) {
				try {
					is.close();
				} catch (IOException e) {
				}
			}
			if (jks != null) {
				try {
					jks.close();
				} catch (IOException e) {
				}
			}
			if (cer != null) {
				try {
					cer.close();
				} catch (IOException e) {
				}
			}
		}
	}

	@Override
	public String getIssuer() {
		return issuer;
	}

	@Override
	public String getServiceUrl() {
		return serviceUrl;
	}

	@Override
	public PrivateKeyEntry getPrivateKeyEntry() {
		return keyInfo.getPrivateKeyEntry();
	}

	@Override
	public PublicKey getPublicKey() {
		return keyInfo.getPublicKey();
	}

	@Override
	public Certificate getCertificate() {
		return certificate; 
	}
}
