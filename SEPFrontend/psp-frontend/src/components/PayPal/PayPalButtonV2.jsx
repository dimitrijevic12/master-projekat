import { PayPalButton } from "react-paypal-button-v2";
import React, { Component } from "react";
import {
  getPayPalTransaction,
  setPayPalTransactionStatus,
} from "../../actions/actionsPayPal";
import { connect } from "react-redux";
import { compose } from "redux";
import Paypal from "./PayPalFunction";

class PayPalButtonV2 extends Component {
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
      <React.Fragment>
        <PayPalButton
          createOrder={(data, actions) => {
            return actions.order.create({
              purchase_units: [
                {
                  description: "Web Shop Transaction",
                  amount: {
                    //currency_code: "USD",
                    value: paypalTransaction.amount,
                  },
                },
              ],
              // application_context: {
              //   shipping_preference: "NO_SHIPPING" // default is "GET_FROM_FILE"
              // }
            });
          }}
          onApprove={async (data, actions) => {
            const order = await actions.order.capture();
            debugger;
            console.log(order);
            await this.props.setPayPalTransactionStatus({
              MerchantOrderId: paypalTransaction.orderId,
              TransactionStatus: "Success",
            });
            window.location.href =
              "https://localhost:3000/perdiem-transaction/" +
              paypalTransaction.orderId;
          }}
          onCancel={async (obj) => {
            await this.props.setPayPalTransactionStatus({
              MerchantOrderId: paypalTransaction.orderId,
              TransactionStatus: "Failed",
            });
            window.location.href = "https://localhost:3000/items";
          }}
          onError={async (err) => {
            console.log(err);
            await this.props.setPayPalTransactionStatus({
              MerchantOrderId: paypalTransaction.orderId,
              TransactionStatus: "Error",
            });
            window.location.href =
              "https://localhost:3000/error-transaction/" +
              paypalTransaction.orderId;
          }}
          options={{
            clientId:
              "AQ7aLujHi-QtZjK3LDKqyHOsQo1A1mdujEgYSR83W3QkM6vYpOwsTBNgCmcu-X3S5yYqCf7knSybUY7u",
            merchantId: "WFKR8VRZ85X2S",
            currency: paypalTransaction.currency,
          }}
        />
        {paypalTransaction.type === "Course" ? (
          <button
            onClick={() => {
              this.goToSubscription();
            }}
            className="btn btn-primary"
            style={{ textAlign: "center" }}
          >
            Go to subscription
          </button>
        ) : null}
      </React.Fragment>
    );
  }

  goToSubscription() {
    window.location =
      "subscription-paypal/" + this.props.paypalTransaction.orderId;
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
)(PayPalButtonV2);
