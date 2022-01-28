import React, { Component } from "react";
import {
  getRequest,
  sendRequest,
  setPaymentId,
} from "../../actions/actionsTransaction";
import { getPaymentTypesForWebShop } from "../../actions/actionsWebShop";
import {
  setTransactionStatus,
  payWithCryptoValute,
  getCrpytoTransaction,
} from "../../actions/actionsCryptoValute";
import { connect } from "react-redux";
import { compose } from "redux";
import { toast } from "react-toastify";
import Switch from "react-switch";
import "react-toastify/dist/ReactToastify.css";

class ChoosePaymentTypes extends Component {
  state = {
    paymentType: "",
    orderId: "",
  };
  async componentDidMount() {
    var url = window.location.pathname;
    var orderId = url.substring(url.lastIndexOf("/") + 1);
    this.setState({ orderId: orderId });
    await this.props.getRequest(orderId);
    await this.props.getPaymentTypesForWebShop(orderId);
    await this.props.getCrpytoTransaction(orderId);
  }
  render() {
    if (
      this.props.paymentTypes === undefined ||
      this.props.cryptoTransaction === undefined
    ) {
      return null;
    }

    const registeredWebShop = this.props.getWebShop;

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
                <select
                  value={this.state.paymentType}
                  class="form-control"
                  onChange={this.handleChange}
                  name="paymentType"
                  placeholder="Choose payment type"
                >
                  <option value=""> Choose payment type</option>
                  {this.props.paymentTypes.map((item, i) => {
                    return item.name === "CryptoValute" ? (
                      <option key={i} value={item.name}>
                        Crypto currency
                      </option>
                    ) : (
                      <option key={i} value={item.name}>
                        {item.name}
                      </option>
                    );
                  })}
                </select>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button
                disabled={this.state.paymentType === ""}
                className="btn btn-lg btn-primary btn-block"
                onClick={() => this.choose()}
              >
                Choose
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

  async choose() {
    debugger;
    if (this.state.paymentType === "PayPal") {
      window.location = "/paypal-page/" + this.state.orderId;
    }
    if (this.state.paymentType === "CryptoValute") {
      var successful = false;
      successful = await this.props.payWithCryptoValute({
        price_amount: this.props.cryptoTransaction.amount,
        price_currency: this.props.cryptoTransaction.currency,
        receive_currency: this.props.cryptoTransaction.currency,
        success_url:
          "https://localhost:3001/paid-transaction/" + this.state.orderId,
        cancel_url:
          "https://localhost:3001/invalid-transaction/" + this.state.orderId,
      });
      debugger;
      if (successful === true) {
        debugger;
        await this.props.setTransactionStatus({
          MerchantOrderId: this.props.cryptoTransaction.orderId,
          TransactionStatus: "Success",
        });
        window.location.href = this.props.cryptoValutePayment.payment_url;
      }
    } else {
      var successful = false;
      successful = await this.props.sendRequest(this.props.request);

      if (successful === true) {
        debugger;
        var url = window.location.pathname;
        var orderId = url.substring(url.lastIndexOf("/") + 1);
        await this.props.setPaymentId({
          OrderId: orderId,
          PaymentId: this.props.payment.paymentId,
        });
        window.location = this.props.payment.paymentUrl;
      } else {
        toast.configure();
        toast.error("Unsuccessful registration!", {
          position: toast.POSITION.TOP_RIGHT,
        });
      }
    }
  }
}

const mapStateToProps = (state) => ({
  request: state.request,
  payment: state.payment,
  paymentTypes: state.paymentTypes,
  cryptoValutePayment: state.cryptoValutePayment,
  cryptoTransaction: state.cryptoTransaction,
});

export default compose(
  connect(mapStateToProps, {
    getRequest,
    sendRequest,
    setPaymentId,
    getPaymentTypesForWebShop,
    payWithCryptoValute,
    setTransactionStatus,
    getCrpytoTransaction,
  })
)(ChoosePaymentTypes);
