package com.huawei.it.saml.assertion;

import org.jdom.Element;
import org.jdom.Namespace;

import com.huawei.it.saml.response.UuidGenerator;

public class AuthnRequest {

	public static final Namespace samlp = Namespace.getNamespace("samlp",
			"urn:oasis:names:tc:SAML:2.0:protocol");
	public static final Namespace saml = Namespace.getNamespace("saml",
			"urn:oasis:names:tc:SAML:2.0:assertion");
	
	private String issuer;
	private String issueInstant;
	
	private String serviceUrl;
	public AuthnRequest(String issuer,String issueInstant,String serviceUrl){
		this.issuer = issuer;
		this.issueInstant = issueInstant;
		this.serviceUrl = serviceUrl;
	}
	
	public Element toXML() {
		Element request = new Element("AuthnRequest");
		request.setAttribute("ID",new UuidGenerator().generate());
		request.setAttribute("Version","2.0");
		request.setAttribute("IssueInstant",this.issueInstant);
		request.setAttribute("ProtocolBinding","urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
		request.setAttribute("AssertionConsumerServiceIndex","0");
		request.setAttribute("AssertionConsumerServiceURL",this.serviceUrl);
		request.setAttribute("AttributeConsumingServiceIndex","0");
	
		Element issuerElement = new Element("Issuer");
		issuerElement.addContent(this.issuer);
		issuerElement.setNamespace(saml);
		
		Element nameIDPolicy = new Element("NameIDPolicy");
		nameIDPolicy.setAttribute("AllowCreate","False");
		nameIDPolicy.setNamespace(samlp);
		
		request.addContent(issuerElement);
		request.addContent(nameIDPolicy);
		request.setNamespace(samlp);
		
		return request;
	}
}
