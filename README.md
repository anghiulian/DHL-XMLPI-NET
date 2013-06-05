DHL-XMLPI-NET
=============
This is an example application for how to connect to DHL XML-PI web service using .Net (csharp) and sending a test request for booking a pickup.

Useful links:
http://xmlpitest-ea.dhl.com/
http://xmlpitest-ea.dhl.com/serviceval/jsps/main/Main_menu.jsp

DHL.cs was created using xsd.exe on the XSD files from the DHL Toolkit(http://xmlpitest-ea.dhl.com/):

xsd -c book-pickup-req_EA.xsd cancel-pickup-req_EA.xsd datatypes.xsd datatypes_EA.xsd DCT-req.xsd DCT-Response.xsd DCTRequestdatatypes.xsd DCTResponsedatatypes.xsd err-res.xsd pickup-err-res.xsd pickup-res.xsd pickupdatatypes_EA.xsd routing-err-res.xsd routing-req_EA.xsd routing-res.xsd ship-val-err-res.xsd ship-val-req_EA.xsd ship-val-res_EA.xsd track-err-res.xsd TrackingRequestKnown.xsd TrackingRequestUnknown.xsd ".\TrackingResponse.xsd"
