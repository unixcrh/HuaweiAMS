package com.huawei.it.saml.servlet;

import java.io.IOException;
import java.nio.charset.StandardCharsets;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.codec.binary.Base64;

import com.huawei.it.saml.SAMLException;
import com.huawei.it.saml.SAMLResponseValidator;
import com.huawei.it.saml.impl.SAMLRequestImpl;
import com.huawei.it.saml.impl.SAMLResponseValidatorImpl;

public class SAMLRequestServlet extends HttpServlet{	
	private static final long serialVersionUID = 6599904768715487694L;
	private static final String returnPage = "/WEB-INF/pages/index.jsp";
	private static final String logon = "/WEB-INF/pages/logon.jsp";
	
	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		try {
			byte[] req = new SAMLRequestImpl().generate().getBytes(StandardCharsets.UTF_8);
			req = new Base64().encode(req);
			request.setAttribute("SAMLRequest", new String(req,StandardCharsets.UTF_8));
		} catch (SAMLException e) {
			e.printStackTrace();
			request.setAttribute("SAMLRequest", e.getMessage());
		}
		request.getRequestDispatcher(returnPage).forward(request, response);
	}
	
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException{
		String resp = request.getParameter("SAMLResponse");
		byte[] buffer = new Base64().decode(resp.getBytes(StandardCharsets.UTF_8));
		try {
			SAMLResponseValidator vld = new SAMLResponseValidatorImpl(new String(buffer,StandardCharsets.UTF_8));
			vld.validate();
			request.setAttribute("uid", vld.getNameId());
		} catch (SAMLException e) {
			e.printStackTrace();
			request.setAttribute("error", e.getMessage());
		}
		request.getRequestDispatcher(logon).forward(request, response);
	}
}
