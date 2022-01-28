import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { getTransactionsForBuyer } from "../../actions/actionsTransaction";
import moment from "moment";

class TransactionsForBuyer extends Component {
  state = {
    showPostModal: false,
    showEditModal: false,
    itemToEdit: {},
  };

  async componentDidMount() {
    debugger;
    await this.props.getTransactionsForBuyer(
      sessionStorage.getItem("userIdWebShop")
    );
  }

  render() {
    if (this.props.transactionsForBuyer === undefined) {
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
                      <th style={{ textAlign: "center" }}>Perdiem</th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.transactionsForBuyer.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>
                          {moment(f.timestamp).format("DD/MM/YYYY HH:mm")}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.totalPrice.toFixed(2)}
                        </td>
                        <td style={{ textAlign: "center" }}>{f.currency}</td>
                        <td style={{ textAlign: "center" }}>{f.seller.name}</td>
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
                        <td style={{ textAlign: "center" }}>
                          {f.perdiemStatus === 1 ? (
                            <button
                              className="btn btn-primary"
                              onClick={() => {
                                this.payPerdiem(f);
                              }}
                            >
                              Pay perdiem
                            </button>
                          ) : null}
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

  payPerdiem(f) {
    window.location.href = "https://localhost:3000/perdiem-transaction/" + f.id;
  }

  view(f) {
    sessionStorage.setItem("transaction-id", f.id);
    window.location = "/transaction/" + f.id;
  }
}

const mapStateToProps = (state) => ({
  transactionsForBuyer: state.transactionsForBuyer,
});

export default compose(connect(mapStateToProps, { getTransactionsForBuyer }))(
  TransactionsForBuyer
);
