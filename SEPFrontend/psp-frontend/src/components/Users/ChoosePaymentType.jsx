import React, { Component } from "react";
import {
    getRequest,
    sendRequest,
    setPaymentId
} from "../../actions/actionsTransaction";
import {
  getPaymentTypesForWebShop,
} from "../../actions/actionsWebShop";
import { connect } from "react-redux";
import { compose } from "redux";
import { toast } from "react-toastify";
import Switch from "react-switch";
import "react-toastify/dist/ReactToastify.css";

class ChoosePaymentTypes extends Component {
  state = {
    paymentType: "",
  };
  async componentDidMount() {
    var url = window.location.pathname;
    var orderId = url.substring(url.lastIndexOf('/') + 1);
    await this.props.getRequest(
        orderId
    );
    await this.props.getPaymentTypesForWebShop(
      orderId
  );

  }
  render() {
    if (this.props.paymentTypes === undefined) {
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
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Choose payment type:</label>
                  <select
                    value={this.state.paymentType}
                    class="form-control"
                    onChange={this.handleChange}
                    name="paymentType"
                  >    
                    <option value=""> </option>
                    {
                      this.props.paymentTypes.map((item, i) => {
                        return (
                          <option key={i} value={item.name}>
                            {item.name}
                          </option>
                        );
                      })
                    }
                  </select>
                </div>
              </div>
              <div className="mt-5 pb-5">
              <button
               disabled={this.state.paymentType === ""}
                className="btn btn-lg btn-primary btn-block"
                onClick={this.choose.bind(this)}
              >
                Choose
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

  async choose() {
    debugger;
    var successful = false;
    successful = await this.props.sendRequest(this.props.request);

    if (successful === true) {
        await this.props.setPaymentId({OrderId: this.props.request.orderId , PaymentId: this.props.payment.paymentId});
        window.location.href = this.props.payment.paymentUrl

    } else {
      toast.configure();
      toast.error("Unsuccessful registration!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }
}

const mapStateToProps = (state) => ({
    request: state.request,
    payment: state.payment,
    paymentTypes: state.paymentTypes
});

export default compose(
  connect(mapStateToProps, {
    getRequest, sendRequest, setPaymentId, getPaymentTypesForWebShop,
  })
)(ChoosePaymentTypes);
