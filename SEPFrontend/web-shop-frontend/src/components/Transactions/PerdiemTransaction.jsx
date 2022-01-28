import React, { Component } from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import { getTransactionById } from "../../actions/actionsTransaction";
import { getCourseById } from "../../actions/actionsCourse";
import { getConferenceById } from "../../actions/actionsConference";
import { payPerdiem } from "../../actions/actionsBank";
import { getRegisteredUserById } from "../../actions/actionsUsers";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

class PerdeimTransaction extends Component {
  state = {
    showPerdiemForm: true,
    uniquePersonalRegistrationNumber: "",
    amount: 0,
    currency: "EUR",
    currencyList: ["EUR", "USD", "CAD"],
    bank: "Erste Bank",
    bankList: ["Erste Bank", "UniCredit Bank"],
  };

  async componentDidMount() {
    debugger;
    var url = window.location.pathname;
    const transactionId = url.substring(url.lastIndexOf("/") + 1);
    await this.props.getRegisteredUserById();
    await this.props.getTransactionById(transactionId);
    await this.shouldDisplayPerdiemForm(this.props.transaction);
  }

  render() {
    if (this.props.transaction === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        <div className="text-center pt-5">
          <h4>Pay perdiem</h4>
        </div>
        {this.state.showPerdiemForm ? (
          <React.Fragment>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="tag">Unique Personal Registration Number:</label>
                  <input
                    type="text"
                    name="uniquePersonalRegistrationNumber"
                    value={this.state.uniquePersonalRegistrationNumber}
                    onChange={this.handleChange}
                    class="form-control"
                    id="uniquePersonalRegistrationNumber"
                    placeholder="Enter Unique Personal Registration Number"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="tag">Amount:</label>
                  <input
                    type="number"
                    name="amount"
                    value={this.state.amount}
                    onChange={this.handleChange}
                    class="form-control"
                    id="amount"
                    placeholder="Enter amount"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Choose currency:</label>
                  <select
                    value={this.state.currency}
                    class="form-control"
                    onChange={this.handleChange}
                    name="currency"
                  >
                    {this.state.currencyList.map((item, i) => {
                      return (
                        <option key={i} value={item}>
                          {item}
                        </option>
                      );
                    })}
                  </select>
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Choose bank:</label>
                  <select
                    value={this.state.bank}
                    class="form-control"
                    onChange={this.handleChange}
                    name="bank"
                  >
                    {this.state.bankList.map((item, i) => {
                      return (
                        <option key={i} value={item}>
                          {item}
                        </option>
                      );
                    })}
                  </select>
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button
                onClick={() => {
                  this.createItem();
                }}
                disabled={
                  this.state.contentPath === "" ||
                  this.state.name === "" ||
                  this.state.price === ""
                }
                className="btn btn-primary btn-block"
              >
                Pay Perdiem
              </button>
            </div>
          </React.Fragment>
        ) : (
          this.redirect()
        )}
      </div>
    );
  }

  async createItem() {
    debugger;
    var successful = false;
    successful = await this.props.payPerdiem({
      uniquePersonalRegistrationNumber:
        this.state.uniquePersonalRegistrationNumber,
      amount: this.state.amount,
      currency: this.state.currency,
      bank: this.state.bank,
      accountNumber: this.props.registeredUser.accountNumber,
    });
    if (successful === true) {
      window.location.href =
        "https://localhost:3000/successful-transaction/" +
        this.props.transaction.id;
    } else {
      toast.configure();
      toast.error("Wrong informations!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };

  redirect() {
    if (this.state.showPerdiemForm === false) {
      window.location.href =
        "https://localhost:3000/successful-transaction/" +
        this.props.transaction.id;
    }
  }

  async shouldDisplayPerdiemForm(transaction) {
    if (transaction === undefined) {
      return null;
    }
    debugger;
    for (var i = 0; i < transaction.transactionItems.length; i++) {
      if (transaction.transactionItems[i].type === 1) {
        await this.props.getCourseById(
          transaction.transactionItems[i].productId
        );
        if (this.props.course.online === false) {
          this.setState({
            showPerdiemForm: true,
          });
          return true;
        }
      }
      if (transaction.transactionItems[i].type === 0) {
        await this.props.getConferenceById(
          transaction.transactionItems[i].productId
        );
        if (this.props.conference.online === false) {
          this.setState({
            showPerdiemForm: true,
          });
          return true;
        }
      }
    }
    this.setState({
      showPerdiemForm: false,
    });
    return false;
  }

  view(f) {
    sessionStorage.setItem("transaction-id", f.id);
    window.location = "/transaction/" + f.id;
  }
}

const mapStateToProps = (state) => ({
  course: state.course,
  conference: state.conference,
  transaction: state.transaction,
  registeredUser: state.registeredUser,
});

export default compose(
  connect(mapStateToProps, {
    getTransactionById,
    getCourseById,
    getConferenceById,
    payPerdiem,
    getRegisteredUserById,
  })
)(PerdeimTransaction);
