import React, { useState } from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import QRCode from "react-qr-code";
import { getPSPRequest } from "../actions/actions";

class QRPayment extends React.Component {
  state = { pan: "", cardHolderName: "", securityCode: "", expirationDate: "" };
  componentWillMount() {
    this.props.getPSPRequest(window.location.pathname.slice(11));
  }
  render() {
    if (this.props.pspRequest === undefined) {
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
              Payment ID: {window.location.pathname.slice(11)}
            </div>
            <div className="pt-3" style={{ fontSize: 20 }}>
              Total: 222.0 RSD
            </div>
            <div>
              <QRCode value={JSON.stringify(this.props.pspRequest)} />
            </div>
            <div className="mt-5" style={{ textAlign: "center" }}>
              <button className="btn btn-lg btn-primary btn-block w-50">
                Pay
              </button>
            </div>
          </div>
        </div>
      </main>
    );
  }
  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };
}

const mapStateToProps = (state) => ({ pspRequest: state });

export default compose(connect(mapStateToProps, { getPSPRequest }))(QRPayment);
