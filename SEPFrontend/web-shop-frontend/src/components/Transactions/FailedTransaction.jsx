import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { getTransactionById } from "../../actions/actionsTransaction";
import moment from "moment";

class FailedTransaction extends Component {
  state = {
    showPostModal: false,
    showEditModal: false,
    itemToEdit: {},
  };

  async componentDidMount() {
    debugger;
    var url = window.location.pathname;
    const transactionId = url.substring(url.lastIndexOf("/") + 1);
    await this.props.getTransactionById(transactionId);
  }

  render() {
    if (this.props.transaction === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        <div className="text-center pt-5">
          <h4>
            Failed Transaction! <img src="/images/test.png" />
          </h4>
        </div>
        <hr />
        <div className="wrap bg-white pt-3">
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <Card
              className="mt-5"
              style={{
                shadowColor: "gray",
                boxShadow: "0 8px 6px -6px #999",
              }}
            >
              <div
                style={{
                  maxHeight: "440px",
                  overflowY: "auto",
                }}
              >
                <Table className="table allPrescriptions mb-0" striped>
                  <thead>
                    <tr>
                      <th style={{ textAlign: "center" }}>Timestamp</th>
                      <th style={{ textAlign: "center" }}>Total price</th>
                      <th style={{ textAlign: "center" }}>Currency</th>
                      <th style={{ textAlign: "center" }}>Seller</th>
                      <th style={{ textAlign: "center" }}>Transaction Items</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td style={{ textAlign: "center" }}>
                        {moment(this.props.transaction.timestamp).format(
                          "DD/MM/YYYY HH:mm"
                        )}
                      </td>
                      <td style={{ textAlign: "center" }}>
                        {this.props.transaction.totalPrice.toFixed(2)}
                      </td>
                      <td style={{ textAlign: "center" }}>
                        {this.props.transaction.currency}
                      </td>
                      <td style={{ textAlign: "center" }}>
                        {this.props.transaction.seller.name}
                      </td>
                      <td style={{ textAlign: "center" }}>
                        <img
                          onClick={() => {
                            this.view(this.props.transaction);
                          }}
                          src="/images/analytics.png"
                        />
                      </td>
                    </tr>
                  </tbody>
                </Table>
              </div>
            </Card>
          </div>
        </div>
      </div>
    );
  }

  view(f) {
    sessionStorage.setItem("transaction-id", f.id);
    window.location = "/transaction/" + f.id;
  }
}

const mapStateToProps = (state) => ({
  transaction: state.transaction,
});

export default compose(connect(mapStateToProps, { getTransactionById }))(
  FailedTransaction
);
