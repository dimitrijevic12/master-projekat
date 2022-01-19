import { PayPalButton } from "react-paypal-button-v2";
import React, { Component } from "react";
import {
  getPayPalTransaction,
  setPayPalTransactionStatus,
} from "../../actions/actionsPayPal";
import { connect } from "react-redux";
import { compose } from "redux";
import Paypal from "./PayPalFunction";

class PayPalButtonSubscription extends Component {
  state = {
    paymentType: "",
    choosenType: "Monthly",
    types: ["Monthly", "Yearly"],
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
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="lastName">Choose type of subscription:</label>
            <select
              value={this.state.choosenType}
              class="form-control"
              onChange={this.handleChange}
              name="choosenType"
            >
              {this.state.types.map((item, i) => {
                return (
                  <option key={i} value={item}>
                    {item}
                  </option>
                );
              })}
            </select>
          </div>
        </div>
        <br />
        <hr />
        <PayPalButton
          amount={paypalTransaction.amount}
          currency={paypalTransaction.currency}
          options={{
            vault: true,
            clientId:
              "AQ7aLujHi-QtZjK3LDKqyHOsQo1A1mdujEgYSR83W3QkM6vYpOwsTBNgCmcu-X3S5yYqCf7knSybUY7u",
          }}
          createSubscription={(data, actions) => {
            return this.state.paymentType === "Monthly"
              ? actions.subscription.create({
                  plan_id: "P-1DN593649J0344710MHPOSUI",
                })
              : actions.subscription.create({
                  plan_id: "P-9BG28056U4335781RMHPO6YI",
                });
          }}
          onApprove={async (data, actions) => {
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
        />
      </React.Fragment>
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

const mapStateToProps = (state) => ({
  paypalTransaction: state.paypalTransaction,
});

export default compose(
  connect(mapStateToProps, {
    getPayPalTransaction,
    setPayPalTransactionStatus,
  })
)(PayPalButtonSubscription);
