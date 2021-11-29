import React, { useState } from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import { getPSPRequest } from "../actions/actions";

class CardPayment extends React.Component {
  state = { pan: "", cardHolderName: "", securityCode: "", expirationDate: "" };
  render() {
    return (
      <main
        className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
        style={{ height: "100vh", backgroundColor: "#82b0fa" }}
      >
        <div className="wrap bg-white p-5 w-50">
          <div className="w-100" style={{ textAlign: "left" }}>
            <div className="text-center" style={{ fontSize: 30 }}>
              Payment ID: {window.location.pathname.slice(5)}
            </div>
            <div className="pt-3" style={{ fontSize: 20 }}>
              Total: 222.0 RSD
            </div>
            <div class="form-group w-100 pr-5 pt-5">
              <label for="pan">PAN:</label>
              <input
                type="text"
                name="pan"
                value={this.state.pan}
                onChange={this.handleChange}
                class="form-control"
                id="pan"
                placeholder="Enter PAN"
              />
            </div>
            <div className="mt-5">
              <div class="form-group w-100 pr-5">
                <label for="pan">Card holder name:</label>
                <input
                  type="text"
                  name="cardHolderName"
                  value={this.state.cardHolderName}
                  onChange={this.handleChange}
                  class="form-control"
                  id="cardHolderName"
                  placeholder="Enter card holder name"
                />
              </div>
            </div>
            <div className="mt-5">
              <div
                className="d-inline-flex w-100"
                style={{ textAlign: "left", paddingRight: "5px" }}
              >
                <div class="form-group w-100" style={{ marginRight: "50px" }}>
                  <label for="pan">Expiry date:</label>
                  <input
                    type="text"
                    name="expirationDate"
                    value={this.state.expirationDate}
                    onChange={this.handleChange}
                    class="form-control"
                    id="expirationDate"
                    placeholder="MM / YY"
                  />
                </div>
                <div class="form-group w-100" style={{ marginLeft: "50px" }}>
                  <label for="pan">Security code:</label>
                  <input
                    type="password"
                    name="securityCode"
                    value={this.state.securityCode}
                    onChange={this.handleChange}
                    class="form-control"
                    id="securityCode"
                    placeholder="Enter security code"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5" style={{ textAlign: "center" }}>
              <button className="btn btn-lg btn-primary btn-block w-50">
                Pay
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
}

const mapStateToProps = (state) => ({ pspRequest: state.pspRequest });

export default compose(connect(mapStateToProps, { getPSPRequest }))(
  CardPayment
);
