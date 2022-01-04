import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { getTransactions } from "../../actions/actionsTransaction";
import moment from "moment";

class Transactions extends Component {
  state = {};

  async componentDidMount() {
    debugger;
    await this.props.getTransactions();
  }

  render() {
    if (this.props.transactions === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        <h3 className="mt-4" style={{ textAlign: "center" }}>
          Previous Transactions
        </h3>
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
                      <th style={{ textAlign: "center" }}>OrderId</th>
                      <th style={{ textAlign: "center" }}>Amount</th>
                      <th style={{ textAlign: "center" }}>Currency</th>
                      <th style={{ textAlign: "center" }}>Timestamp</th>
                      <th style={{ textAlign: "center" }}>Status</th>
                      <th style={{ textAlign: "center" }}>MerchantName</th>
                      <th style={{ textAlign: "center" }}>IssuerName</th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.transactions.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>{f.orderId}</td>
                        <td style={{ textAlign: "center" }}>
                          {f.amount.toFixed(2)}
                        </td>
                        <td style={{ textAlign: "center" }}>{f.currency}</td>
                        <td style={{ textAlign: "center" }}>
                          {moment(f.timestamp).format("DD/MM/YYYY HH:mm")}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.transactionStatus === 0
                            ? "Pending"
                            : f.status === 1
                            ? "Success"
                            : f.status === 2
                            ? "Failed"
                            : "Error"}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.merchantName}
                        </td>
                        <td style={{ textAlign: "center" }}>{f.issuerName}</td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </div>
            </Card>
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => ({
  transactions: state.transactions,
});

export default compose(connect(mapStateToProps, { getTransactions }))(
  Transactions
);
