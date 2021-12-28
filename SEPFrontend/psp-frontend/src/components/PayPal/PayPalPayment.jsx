import React, { Component } from "react";
import {
  getPayPalTransaction,
  setPayPalTransactionStatus,
} from "../../actions/actionsPayPal";
import { connect } from "react-redux";
import { compose } from "redux";
import Paypal from "./PayPalFunction";

class PayPalPayment extends Component {
  state = {
    paymentType: "",
  };
  async componentDidMount() {
    var url = window.location.pathname;
    var orderId = url.substring(url.lastIndexOf("/") + 1);
    await this.props.getPayPalTransaction(orderId);
  }
  render() {
    if (this.props.paypalTransaction === undefined) {
      return null;
    }

    const paypalTransaction = this.props.paypalTransaction;

    return (
      <main
        className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
        style={{ height: "100vh", backgroundColor: "#82b0fa" }}
      >
        <div className="wrap bg-white p-5 w-50">
          <div className="w-100" style={{ textAlign: "left" }}></div>
          <div className="text-center">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <Paypal />
              </div>
            </div>
          </div>
        </div>
      </main>
    );
  }
}

const mapStateToProps = (state) => ({
  paypalTransaction: state.paypalTransaction,
});

export default compose(
  connect(mapStateToProps, {
    getPayPalTransaction,
    setPayPalTransactionStatus,
  })
)(PayPalPayment);
