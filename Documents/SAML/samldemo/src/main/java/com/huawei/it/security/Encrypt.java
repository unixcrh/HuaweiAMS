package com.huawei.it.security;

import java.nio.charset.StandardCharsets;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;
import javax.crypto.spec.SecretKeySpec;

import org.apache.commons.codec.binary.Base64;

public final class Encrypt {
	private static final String strKey = "key4kerb";

	public static String encryptWithDES(String strToEncrypt)
			throws EncryptException {
		SecretKey deskey = new SecretKeySpec(strKey.getBytes(StandardCharsets.UTF_8), "DES");;
		try {
			Cipher cipher = Cipher.getInstance("DES");
			cipher.init(Cipher.ENCRYPT_MODE, deskey);
			byte[] bEncoded = cipher.doFinal(strToEncrypt.getBytes(StandardCharsets.UTF_8));
			return new String(new Base64().encode(bEncoded),StandardCharsets.UTF_8);
		} catch (NoSuchAlgorithmException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (NoSuchPaddingException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (InvalidKeyException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (IllegalBlockSizeException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (BadPaddingException e) {
			throw new EncryptException(e.getMessage(),e);
		}
	}

	public static String decryptWithDES(String strToDecrypt)
			throws EncryptException {
		SecretKey deskey = new SecretKeySpec(strKey.getBytes(StandardCharsets.UTF_8), "DES");
		try {
			Cipher cipher = Cipher.getInstance("DES");
			cipher.init(Cipher.DECRYPT_MODE, deskey);
			byte[] bEncodedDeB64ed = new Base64().decode(strToDecrypt.getBytes(StandardCharsets.UTF_8));
			byte[] bDecoded = cipher.doFinal(bEncodedDeB64ed);
			return new String(bDecoded,StandardCharsets.UTF_8);
		} catch (NoSuchAlgorithmException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (NoSuchPaddingException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (InvalidKeyException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (IllegalBlockSizeException e) {
			throw new EncryptException(e.getMessage(),e);
		} catch (BadPaddingException e) {
			throw new EncryptException(e.getMessage(),e);
		}
	}
}
