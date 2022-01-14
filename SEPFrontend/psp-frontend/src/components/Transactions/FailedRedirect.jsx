import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { setTransactionStatus } from "../../actions/actionsCryptoValute";
import moment from "moment";

class FailedRedirect extends Component {
  state = {};

  async componentDidMount() {
    debugger;
    var url = window.location.pathname;
    var orderId = url.substring(url.lastIndexOf("/") + 1);
    await this.props.setTransactionStatus({
        MerchantOrderId: orderId,
        TransactionStatus: "Failed",
      });
    window.location.href =
            "http://localhost:3000/failed-transaction/" +
            orderId;
  }

  render() {
    debugger;
    return (
      <div>
        
      </div>
    );
  }
}

const mapStateToProps = (state) => ({
 
});

export default compose(connect(mapStateToProps, { setTransactionStatus }))(
    FailedRedirect
);