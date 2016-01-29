package com.huawei.it.saml;

public interface SAMLResponseValidator {
	public void validate() throws SAMLException;
	public String getNameId();
}
