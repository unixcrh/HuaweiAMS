package com.huawei.it.saml.response;

import java.util.Random;

public class UuidGenerator {
	private static final char[] charMapping = { '0', '1', '2', '3', '4', '5',
		'6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
	
	public String generate() {
		byte[] bytes = new byte[20];
		Random random = new Random();
		random.nextBytes(bytes);
		char[] chars = new char[40];
		for (int i = 0; i < bytes.length; i++) {
			int left = (bytes[i] >> 4) & 0x0f;
			int right = bytes[i] & 0x0f;
			chars[i * 2] = charMapping[left];
			chars[i * 2 + 1] = charMapping[right];
		}
		return "s2" + String.valueOf(chars);
	}
}
