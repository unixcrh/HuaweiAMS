package com.huawei.it.saml;

import org.w3c.dom.Node;

public interface SAMLResponse {

	public String getIssuer();

	public String getNotBefore();

	public String getNotOnOrAfter();

	public Node getSignature();

	public String getNameId();
}
