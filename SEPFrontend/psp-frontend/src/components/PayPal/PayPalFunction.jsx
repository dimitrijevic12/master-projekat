import React, { useRef, useEffect, useState } from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import {
  setPayPalTransactionStatus,
  getPayPalTransaction,
} from "../../actions/actionsPayPal";

function PayPalFunction(props) {
  const paypal = useRef();
  const [item, setItem] = useState({});
  debugger;

  useEffect(async () => {
    var url = window.location.pathname;
    var orderId = url.substring(url.lastIndexOf("/") + 1);
    await props.getPayPalTransaction(orderId);
    window.paypal
      .Buttons({
        createOrder: (data, actions, err) => {
          return actions.order.create({
            intent: "CAPTURE",
            purchase_units: [
              {
                description: "Web Shop Transaction",
                amount: {
                  currency_code: "USD",
                  value: props.paypalTransaction.amount,
                },
              },
            ],
            redirect_urls: {
              return_url: "https://example.com",
              cancel_url: "https://localhost:3000/login",
            },
          });
        },
        onApprove: async (data, actions) => {
          const order = await actions.order.capture();
          debugger;
          console.log(order);
          await props.setPayPalTransactionStatus({
            MerchantOrderId: props.paypalTransaction.orderId,
            TransactionStatus: "Success",
          });
          window.location.href =
            "https://localhost:3000/successful-transaction/" +
            props.paypalTransaction.orderId;
        },
        onCancel: async (obj) => {
          await props.setPayPalTransactionStatus({
            MerchantOrderId: props.paypalTransaction.orderId,
            TransactionStatus: "Failed",
          });
          window.location.href = "https://localhost:3000/items";
        },
        onError: async (err) => {
          console.log(err);
          await props.setPayPalTransactionStatus({
            MerchantOrderId: props.paypalTransaction.orderId,
            TransactionStatus: "Error",
          });
          window.location.href =
            "https://localhost:3000/error-transaction/" +
            props.paypalTransaction.orderId;
        },
      })
      .render(paypal.current);
  }, []);

  if (props.paypalTransaction === undefined) {
    return null;
  }

  return (
    <div>
      <h4>Your total amount is: {props.paypalTransaction.amount} USD</h4>
      <div ref={paypal}></div>
    </div>
  );
}

const mapStateToProps = (state) => ({
  paypalTransaction: state.paypalTransaction,
});

export default compose(
  connect(mapStateToProps, {
    setPayPalTransactionStatus,
    getPayPalTransaction,
  })
)(PayPalFunction);
