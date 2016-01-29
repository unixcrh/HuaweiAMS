package com.huawei.it.saml.response;

import java.security.KeyStore.PrivateKeyEntry;

import org.jdom.Document;
import org.jdom.output.XMLOutputter;

import com.huawei.it.saml.SAMLException;
import com.huawei.it.saml.assertion.AuthnRequest;

public class RequestBuilder {
	private String issuer;
	private String serviceUrl;
	private PrivateKeyEntry privateKeyEntry;

	public RequestBuilder(String issuer,String serviceUrl,PrivateKeyEntry privateKeyEntry) {
		this.issuer = issuer;
		this.serviceUrl = serviceUrl;
		this.privateKeyEntry = privateKeyEntry;
	}
	
	public String build() throws SAMLException {
		AuthnRequest request = new AuthnRequest(this.issuer,
				this.issuer, this.serviceUrl);
		Request rqeuest = new Request(request,this.privateKeyEntry);		
		Document doc = new Document().setRootElement(rqeuest.toXML());
		return new XMLOutputter().outputString(doc);

	}
}
