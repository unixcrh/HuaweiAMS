package com.huawei.it.saml;

import java.security.KeyStore.PrivateKeyEntry;
import java.security.PublicKey;
import java.security.cert.Certificate;

public interface ServiceProvider {
	public String getIssuer();
	public String getServiceUrl();
	public PrivateKeyEntry getPrivateKeyEntry();
	public PublicKey getPublicKey();
	public Certificate getCertificate();
}
