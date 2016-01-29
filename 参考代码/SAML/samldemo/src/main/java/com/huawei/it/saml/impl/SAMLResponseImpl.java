package com.huawei.it.saml.impl;

import java.io.ByteArrayInputStream;
import java.io.IOException;

import javax.xml.crypto.dsig.XMLSignature;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import com.huawei.it.saml.SAMLException;
import com.huawei.it.saml.SAMLResponse;

public class SAMLResponseImpl implements SAMLResponse {
	private String issuer;
	private String notBefore;
	private String notOnOrAfter;
	private Node signature;
	private String nameId;

	public SAMLResponseImpl(String response) throws SAMLException {
		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		dbf.setNamespaceAware(true);
		Document doc;
		try {
			ByteArrayInputStream bais = new ByteArrayInputStream(
					response.getBytes("UTF-8"));
			doc = dbf.newDocumentBuilder().parse(bais);
			doc.getDocumentElement().setIdAttribute("ID", true);
		} catch (SAXException e) {
			throw new SAMLException("Invalid SAML response", e);
		} catch (IOException e) {
			throw new SAMLException("Invalid SAML response", e);
		} catch (ParserConfigurationException e) {
			throw new SAMLException("Invalid SAML response", e);
		}
		String issuer = getIssuer(doc);
		if (issuer != null) {
			this.issuer = issuer;
		} else {
			throw new SAMLException("Invalid SAML response: miss <Issuer>");
		}
		Element condition = getCondition(doc);
		if (condition != null) {
			this.notBefore = condition.getAttribute("NotBefore");
			this.notOnOrAfter = condition.getAttribute("NotOnOrAfter");
		} else {
			throw new SAMLException("Invalid SAML response: miss <Conditions>");
		}
		Node signature = getSignature(doc);
		if (signature != null) {
			this.signature = signature;
		} else {
			throw new SAMLException("Invalid SAML response: miss <Signature>");
		}
		String nameId = getNameId(doc);
		if (nameId != null) {
			this.nameId = nameId;
		} else {
			throw new SAMLException("Invalid SAML response: miss <NameId>");
		}
	}

	public String getIssuer() {
		return issuer;
	}

	public String getNotBefore() {
		return notBefore;
	}

	public String getNotOnOrAfter() {
		return notOnOrAfter;
	}

	public Node getSignature() {
		return signature;
	}

	public String getNameId() {
		return nameId;
	}

	private String getIssuer(Document doc) {
		NodeList nl = doc.getElementsByTagNameNS("*", "Issuer");
		if (nl.getLength() != 0) {
			return nl.item(0).getTextContent();
		}
		return null;
	}

	private Element getCondition(Document doc) throws SAMLException {
		NodeList nl = doc.getElementsByTagNameNS("*", "Conditions");
		if (nl.getLength() != 0) {
			return (Element) nl.item(0);
		}
		return null;
	}

	private Node getSignature(Document doc) {
		NodeList nl = doc.getElementsByTagNameNS(XMLSignature.XMLNS,
				"Signature");
		if (nl.getLength() != 0) {
			return nl.item(0);
		}
		return null;
	}

	private String getNameId(Document doc) {
		NodeList nl = doc.getElementsByTagNameNS("*", "NameID");
		if (nl.getLength() != 0) {
			return nl.item(0).getTextContent();
		}
		return null;
	}
}
