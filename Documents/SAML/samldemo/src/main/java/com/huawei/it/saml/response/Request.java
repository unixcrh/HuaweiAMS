package com.huawei.it.saml.response;

import java.security.KeyStore.PrivateKeyEntry;

import org.jdom.Document;
import org.jdom.Element;
import org.w3c.dom.NodeList;

import com.huawei.it.saml.SAMLException;
import com.huawei.it.saml.SAMLUtil;
import com.huawei.it.saml.assertion.AuthnRequest;

public class Request {
	private AuthnRequest request;
	private PrivateKeyEntry privateKey;

	public Request(AuthnRequest request, PrivateKeyEntry privateKey) {
		this.request = request;
		this.privateKey = privateKey;
	}

	public Element toXML() throws SAMLException {
		Element req = request.toXML();
		
		if(privateKey != null) {
			Document doc = new Document();
			doc.setRootElement(req);
			org.w3c.dom.Element element = SAMLUtil.toDom(req);
			NodeList nodeList = element.getElementsByTagNameNS(AuthnRequest.saml.getURI(),"Issuer");
			org.w3c.dom.Node insertBefore = nodeList.item(nodeList.getLength() - 1);
			SAMLUtil.insertSignature(element, privateKey, insertBefore);
			req = SAMLUtil.toJdom(element);
			req.detach();
		}
		return req;
	}

}
