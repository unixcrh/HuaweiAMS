package com.huawei.it.saml;

public class SAMLException extends Exception {
	private static final long serialVersionUID = 1L;

	public SAMLException() {
	}

	public SAMLException(Throwable e) {
		super(e);
	}

	public SAMLException(String message) {
		super(message);
	}

	public SAMLException(String message, Throwable e) {
		super(message, e);
	}
}