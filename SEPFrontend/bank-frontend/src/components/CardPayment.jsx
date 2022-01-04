import React from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import {
  getPSPRequest,
  postTransaction,
  getMerchantByMerchantId,
} from "../actions/actions";

class CardPayment extends React.Component {
  state = {
    pan: "",
    panDisplay: "",
    cardHolderName: "",
    securityCode: "",
    expirationDate: "",
    panError: false,
    expirationDateError: false,
    securityCodeError: false,
  };

  async componentWillMount() {
    debugger;
    await this.props.getPSPRequest(window.location.pathname.slice(-36));
    this.props.getMerchantByMerchantId(this.props.pspRequest.merchantId);
  }

  render() {
    if (this.props.pspRequest === undefined) {
      return null;
    }
    debugger;
    return (
      <main
        className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
        style={{ height: "100vh", backgroundColor: "#82b0fa" }}
      >
        <div className="wrap bg-white p-5 w-50">
          <div className="w-100" style={{ textAlign: "left" }}>
            <div className="text-center" style={{ fontSize: 30 }}>
              Payment ID: {window.location.pathname.slice(-36)}
            </div>
            <div className="pt-3" style={{ fontSize: 20 }}>
              Total:{" "}
              {this.props.pspRequest.amount.toFixed(2) +
                " " +
                this.props.pspRequest.currency}
            </div>
            <div class="form-group w-100 pr-5 pt-5">
              <label for="pan">PAN:</label>
              <input
                type="text"
                name="pan"
                value={this.state.panDisplay}
                onChange={this.handlePANChange}
                class="form-control"
                id="pan"
                placeholder="Enter PAN"
                pattern="[0-9]{16}"
                maxLength={16}
                onBlur={this.handleBlurPAN}
                style={this.style(this.state.panError)}
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
                    pattern="((0[1-9])|(1[0-2]))\/[0-9]{2}"
                    maxLength={5}
                    onBlur={this.handleBlurExpirationDate}
                    style={this.style(this.state.expirationDateError)}
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
                    pattern="[0-9]{4}"
                    maxLength={4}
                    onBlur={this.handleBlurSecurityCode}
                    style={this.style(this.state.securityCodeError)}
                  />
                </div>
              </div>
            </div>
            <div className="mt-5" style={{ textAlign: "center" }}>
              <button
                disabled={
                  this.state.panError === true ||
                  this.state.cardHolderName === "" ||
                  this.state.securityCodeError === true ||
                  this.state.expirationDateError === true
                }
                className="btn btn-lg btn-primary btn-block w-50"
                onClick={() => this.pay()}
              >
                Pay
              </button>
            </div>
          </div>
        </div>
      </main>
    );
  }
  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };

  hidePAN = (value) => {
    let display = "";
    for (let i = 0; i < value.length; i++) {
      if (i > 5 && i < 12) {
        display = display.concat("*");
      } else {
        display = display.concat(value[i]);
      }
    }
    return display;
  };

  handlePANChange = (event) => {
    debugger;
    if (event.target.value.length < this.state.pan.length) {
      let test = this.state.pan.slice(0, event.target.value.length);
      let test2 = this.state.panDisplay.slice(0, event.target.value.length);
      this.setState({
        pan: this.state.pan.slice(0, event.target.value.length),
        panDisplay: test2,
      });
      return;
    }
    let display = "";
    for (let i = 0; i < event.target.value.length; i++) {
      if (i > 5 && i < 12) {
        display = display.concat("*");
      } else {
        display = display.concat(event.target.value[i]);
      }
    }
    let pan = this.state.pan.concat(
      event.target.value[event.target.value.length - 1]
    );
    this.setState({ pan: pan, panDisplay: display });
  };

  handleBlurPAN = (event) => {
    debugger;
    let regex = new RegExp("[0-9]{16}");
    if (regex.test(this.state.pan)) {
      this.setState({ panError: false });
    } else {
      this.setState({ panError: true });
    }
  };

  handleBlurExpirationDate = (event) => {
    debugger;
    if (event.target.validity.patternMismatch) {
      this.setState({ expirationDateError: true });
    } else {
      this.setState({ expirationDateError: false });
    }
  };

  handleBlurSecurityCode = (event) => {
    debugger;
    if (event.target.validity.patternMismatch) {
      this.setState({ securityCodeError: true });
    } else {
      this.setState({ securityCodeError: false });
    }
  };

  style(error) {
    if (error) {
      return {
        borderColor: "rgba(255, 0, 0, 0.5)",
        borderWidth: 5,
      };
    }
  }

  async pay() {
    await this.props.postTransaction({
      PaymentId: window.location.pathname.slice(-36),
      PAN: this.state.pan,
      SecurityCode: this.state.securityCode,
      CardHolderName: this.state.cardHolderName,
      ExpirationDate: this.state.expirationDate,
      Amount: this.props.pspRequest.amount,
      AcquirerAccountNumber: this.props.merchant.acquirerAccountNumber,
      AcquirerName: this.props.merchant.acquirerName,
      successUrl: this.props.pspRequest.successUrl,
      failedUrl: this.props.pspRequest.failedUrl,
      errorUrl: this.props.pspRequest.errorUrl,
    });
  }
}

const mapStateToProps = (state) => ({
  pspRequest: state.pspRequest,
  merchant: state.merchant,
});

export default compose(
  connect(mapStateToProps, {
    getPSPRequest,
    postTransaction,
    getMerchantByMerchantId,
  })
)(CardPayment);
