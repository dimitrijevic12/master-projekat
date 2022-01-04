import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { getTransactionsForSeller } from "../../actions/actionsTransaction";
import moment from "moment";

class TransactionsForSeller extends Component {
  state = {
    showPostModal: false,
    showEditModal: false,
    itemToEdit: {},
  };

  async componentDidMount() {
    debugger;
    await this.props.getTransactionsForSeller(
      sessionStorage.getItem("userIdWebShop")
    );
  }

  render() {
    if (this.props.transactionsForSeller === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        <h3 className="mt-4" style={{ textAlign: "center" }}>
          Your Previous Transactions
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
                      <th style={{ textAlign: "center" }}>Timestamp</th>
                      <th style={{ textAlign: "center" }}>Total price</th>
                      <th style={{ textAlign: "center" }}>Currency</th>
                      <th style={{ textAlign: "center" }}>Seller</th>
                      <th style={{ textAlign: "center" }}>Status</th>
                      <th style={{ textAlign: "center" }}>Transaction Items</th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.transactionsForSeller.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>
                          {moment(f.timestamp).format("DD/MM/YYYY HH:mm")}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.totalPrice.toFixed(2)}
                        </td>
                        <td style={{ textAlign: "center" }}>{f.currency}</td>
                        <td style={{ textAlign: "center" }}>
                          {f.buyer.firstName + " " + f.buyer.lastName}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.status === 0
                            ? "Pending"
                            : f.status === 1
                            ? "Success"
                            : f.status === 2
                            ? "Failed"
                            : "Error"}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          <img
                            onClick={() => {
                              this.view(f);
                            }}
                            src="/images/analytics.png"
                          />
                        </td>
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

  view(f) {
    sessionStorage.setItem("transaction-id", f.id);
    window.location = "/transaction/" + f.id;
  }
}

const mapStateToProps = (state) => ({
  transactionsForSeller: state.transactionsForSeller,
});

export default compose(connect(mapStateToProps, { getTransactionsForSeller }))(
  TransactionsForSeller
);
