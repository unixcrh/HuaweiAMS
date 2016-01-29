package com.huawei.it.keystore;

import java.io.IOException;
import java.io.InputStream;
import java.security.Key;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.UnrecoverableEntryException;
import java.security.UnrecoverableKeyException;
import java.security.KeyStore.PrivateKeyEntry;
import java.security.cert.Certificate;
import java.security.cert.CertificateException;

public class KeystoreInfo {
	PublicKey publicKey = null;
	PrivateKeyEntry privateKeyEntry = null;

	public KeystoreInfo(InputStream jks, char[] storepass, char[] keypass, String alias)
			throws KeyInfoException {
		KeyStore ks = null;
		try {
			ks = KeyStore.getInstance("JKS");
			ks.load(jks, storepass);
			privateKeyEntry = (KeyStore.PrivateKeyEntry) ks.getEntry(alias,
					new KeyStore.PasswordProtection(keypass));
			if (ks.containsAlias(alias)) {
				Key key = ks.getKey(alias, keypass);
				if (key instanceof PrivateKey) {
					Certificate cert = ks.getCertificate(alias);
					publicKey = cert.getPublicKey();
				} else {
					throw new KeyInfoException("Private Key Not Exist");
				}
			} else {
				throw new KeyInfoException("Alias Not Exist");
			}
		} catch (KeyStoreException e) {
			throw new KeyInfoException(e.getMessage(),e);
		} catch (NoSuchAlgorithmException e) {
			throw new KeyInfoException(e.getMessage(),e);
		} catch (CertificateException e) {
			throw new KeyInfoException(e.getMessage(),e);
		} catch (IOException e) {
			throw new KeyInfoException(e.getMessage(),e);
		} catch (UnrecoverableKeyException e) {
			throw new KeyInfoException(e.getMessage(),e);
		} catch (UnrecoverableEntryException e) {
			throw new KeyInfoException(e.getMessage(),e);
		} 
	}

	public PrivateKeyEntry getPrivateKeyEntry() {
		return privateKeyEntry;
	}

	public PublicKey getPublicKey() {
		return publicKey;
	}

}
