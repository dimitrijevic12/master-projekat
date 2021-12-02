import React, { Component } from "react";
import {
    getWebShop,
    changePaymentTypes,
    getPaymentTypes
} from "../../actions/actionsWebShop";
import { connect } from "react-redux";
import { compose } from "redux";
import { toast } from "react-toastify";
import Switch from "react-switch";
import "react-toastify/dist/ReactToastify.css";

class ChangePaymentTypes extends Component {
  state = {
    payPal: false,
    cryptoValute: false,
    bank: false,
    payPalId: "",
    cryptoValuteId: "",
    bankId: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getWebShop({
        webShopEmail: sessionStorage.getItem("emailPSP"),
    });

    await this.props.getPaymentTypes();

    debugger;
    var paypalAtMoment = this.props.registeredWebShop.paymentTypes.some(p => p.name === "PayPal");
    var cryptovaluteAtMoment = this.props.registeredWebShop.paymentTypes.some(p => p.name === "CryptoValute");
    var bankAtMoment = this.props.registeredWebShop.paymentTypes.some(p => p.name === "Bank");

    var paypal = this.props.paymentTypes.filter(p => p.name === "PayPal");
    var cryptovalute = this.props.paymentTypes.filter(p => p.name === "CryptoValute");
    var bank = this.props.paymentTypes.filter(p => p.name === "Bank");

    this.setState({
        payPal:
        paypalAtMoment,
        cryptoValute:
        cryptovaluteAtMoment,
        bank: bankAtMoment,
        payPalId: paypal[0].id,
        cryptoValuteId: cryptovalute[0].id,
        bankId: bank[0].id,
    });
  }
  render() {
    if (this.props.registeredWebShop === undefined || this.props.paymentTypes == undefined) {
      return null;
    }

    const registeredWebShop = this.props.getWebShop;

    return (
    
      <main
        className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
        style={{ height: "100vh", backgroundColor: "#82b0fa" }}
      >
        <div className="wrap bg-white p-5 w-50">
          <div className="w-100" style={{ textAlign: "left" }}></div>
              <div className="text-center">
                <h2>Change your payment types </h2>
                <br />
                <label htmlFor="normal-switch" className="pt-4">
                  <span>Paypal</span>
                  <Switch
                    onChange={this.handleChangePayPal}
                    checked={this.state.payPal}
                    className="react-switch"
                    id="normal-switch"
                  />
                </label>
                <br />
                <label htmlFor="normal-switch" className="pt-4">
                  <span>Crypto Valute</span>
                  <Switch
                    onChange={this.handleChangeCryptoValute}
                    checked={this.state.cryptoValute}
                    className="react-switch"
                    id="normal-switch"
                  />
                </label>
                <br />
                <label htmlFor="normal-switch" className="pt-4">
                  <span>Bank</span>
                  <Switch
                    onChange={this.handleChangeBank}
                    checked={this.state.bank}
                    className="react-switch"
                    id="normal-switch"
                  />
                </label>       
                <br />
                <button
                  className="btn btn-primary btn-block mt-4"
                  onClick={() => {
                    this.editPaymentTypes();
                  }}
                >
                  Change
                </button>
              
            </div>
        </div>
      </main>
    );
  }

  handleChangePayPal = (checked) => {
    debugger;
    this.setState({ payPal: checked });
  };

  handleChangeCryptoValute = (checked) => {
    debugger;
    this.setState({ cryptoValute: checked });
  };

  handleChangeBank = (checked) => {
    debugger;
    this.setState({ bank: checked });
  };

  async editPaymentTypes() {
      debugger;
      var successful = false;
    successful = await this.props.changePaymentTypes({
      RegisteredWebShopId: sessionStorage.getItem("userIdPSP"),
      PayPal: { item1: this.state.payPalId , item2: this.state.payPal},
      CryptoValute: { item1: this.state.cryptoValuteId , item2: this.state.cryptoValute},
      Bank: { item1: this.state.bankId , item2: this.state.bank},
    });
    //window.location = "/paymenttypes";
    if (successful === true) {
        toast.configure();
        toast.success("Successfully edited payment types!", {
          position: toast.POSITION.TOP_RIGHT,
        });
      } else {
        toast.configure();
        toast.error("Unsuccessfully edited payment types!", {
          position: toast.POSITION.TOP_RIGHT,
        });
      }
  }
}

const mapStateToProps = (state) => ({
    registeredWebShop: state.registeredWebShop,
    paymentTypes: state.paymentTypes,
});

export default compose(
  connect(mapStateToProps, {
    getWebShop,
    changePaymentTypes,
    getPaymentTypes,
  })
)(ChangePaymentTypes);
