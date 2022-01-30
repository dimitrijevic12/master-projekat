import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { setTransactionStatus } from "../../actions/actionsCryptoValute";
import moment from "moment";

class SuccessRedirect extends Component {
  state = {};

  async componentDidMount() {
    debugger;
    var url = window.location.pathname;
    var orderId = url.substring(url.lastIndexOf("/") + 1);
    await this.props.setTransactionStatus({
      MerchantOrderId: orderId,
      TransactionStatus: "Success",
    });
    window.location.href =
    `${process.env.REACT_APP_WEBSHOP_FRONT_END_URL}perdiem-transaction/` + orderId;
  }

  render() {
    debugger;
    return <div></div>;
  }
}

const mapStateToProps = (state) => ({});

export default compose(connect(mapStateToProps, { setTransactionStatus }))(
  SuccessRedirect
);
