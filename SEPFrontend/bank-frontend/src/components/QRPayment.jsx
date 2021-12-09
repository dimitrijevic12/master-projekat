import React from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import QRCode from "react-qr-code";
import {
  getPSPRequest,
  postTransaction,
  getMerchantByMerchantId,
} from "../actions/actions";

class QRPayment extends React.Component {
  state = { pan: "", cardHolderName: "", securityCode: "", expirationDate: "" };

  async componentWillMount() {
    debugger;
    await this.props.getPSPRequest(window.location.pathname.slice(-36));
    this.props.getMerchantByMerchantId(this.props.pspRequest.merchantId);
  }

  render() {
    if (
      this.props.pspRequest === undefined ||
      this.props.merchant === undefined
    ) {
      return null;
    }
    return (
      <main
        className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
        style={{ height: "100vh", backgroundColor: "#82b0fa" }}
      >
        <div className="wrap bg-white p-5 w-50">
          <div className="w-100" style={{ textAlign: "left" }}>
            <div className="text-center" style={{ fontSize: 30 }}>
              Payment ID: {window.location.pathname.slice(-36)}
            </div>
            <div className="pt-3" style={{ fontSize: 20 }}>
              Total: {this.props.pspRequest.amount} EUR
            </div>
            <div className="text-center pt-5">
              <QRCode
                value={JSON.stringify({
                  paymentId: window.location.pathname.slice(-36),
                  acquirerAccountNumber:
                    this.props.merchant.acquirerAccountNumber,
                  AcquirerName: this.props.merchant.acquirerName,
                  Amount: this.props.pspRequest.amount,
                  Currency: "EUR",
                })}
              />
            </div>
            <div className="mt-5" style={{ textAlign: "center" }}>
              <button
                className="btn btn-lg btn-primary btn-block w-50"
                onClick={() => this.pay()}
              >
                Pay
              </button>
            </div>
          </div>
        </div>
      </main>
    );
  }
  async pay() {
    await this.props.postTransaction({
      PaymentId: window.location.pathname.slice(-36),
      PAN: "1234561234561234",
      SecurityCode: "1234",
      CardHolderName: "Holder Name",
      ExpirationDate: "04/22",
      Amount: this.props.pspRequest.amount,
      AcquirerAccountNumber: this.props.merchant.acquirerAccountNumber,
      AcquirerName: this.props.merchant.acquirerName,
      Amount: this.props.pspRequest.amount,
      successUrl: this.props.pspRequest.successUrl,
      failedUrl: this.props.pspRequest.failedUrl,
      errorUrl: this.props.pspRequest.errorUrl,
    });
  }
}

const mapStateToProps = (state) => ({
  pspRequest: state.pspRequest,
  merchant: state.merchant,
});

export default compose(
  connect(mapStateToProps, {
    getPSPRequest,
    postTransaction,
    getMerchantByMerchantId,
  })
)(QRPayment);
