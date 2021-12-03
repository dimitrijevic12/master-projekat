import React, { Component } from "react";
import {
  adminRegistration,
  bankAdminRegistration,
  editAdmin,
  pspAdminRegistration,
} from "../../actions/actionsUsers";
import { connect } from "react-redux";
import { toast } from "react-toastify";
import { compose } from "redux";
import "react-toastify/dist/ReactToastify.css";

class AdminRegistration extends Component {
  state = {
    id: 1,
    firstName: "",
    email: "",
    username: "",
    password: "",
    repeatPassword: "",
  };

  render() {
    return (
      <React.Fragment>
        <main className="main pt-0 pb-0" style={{ backgroundColor: "#4da3ff" }}>
          <div className="wrap bg-white">
            <div className="text-center pt-5">
              <img
                alt=""
                width="100"
                height="100"
                src="/images/iconfinder_00-ELASTOFONT-STORE-READY_user-circle_2703062.png"
              />
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Name:</label>
                  <input
                    type="text"
                    name="firstName"
                    value={this.state.firstName}
                    onChange={this.handleChange}
                    class="form-control"
                    id="firstName"
                    placeholder="Enter name"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Username:</label>
                  <input
                    type="text"
                    name="username"
                    value={this.state.username}
                    onChange={this.handleChange}
                    class="form-control"
                    id="username"
                    placeholder="Enter username"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Email:</label>
                  <input
                    type="text"
                    name="email"
                    value={this.state.email}
                    onChange={this.handleChange}
                    class="form-control"
                    id="email"
                    placeholder="Enter email"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Password:</label>
                  <input
                    type="password"
                    name="password"
                    value={this.state.password}
                    onChange={this.handleChange}
                    class="form-control"
                    id="password"
                    placeholder="Enter password"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Repeat password:</label>
                  <input
                    type="password"
                    name="repeatPassword"
                    value={this.state.repeatPassword}
                    onChange={this.handleChange}
                    class="form-control"
                    id="repeatPassword"
                    placeholder="Repeat password"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button
                disabled={this.state.password != this.state.repeatPassword}
                className="btn btn-lg btn-primary btn-block"
                onClick={this.register.bind(this)}
              >
                Register
              </button>
            </div>
          </div>
        </main>
      </React.Fragment>
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

  handleChangeDate = (e) => {
    this.setState({
      dateOfBirth: e,
    });
  };

  async register() {
    debugger;
    var successful = false;
    successful = await this.props.adminRegistration({
      Username: this.state.username,
      Email: this.state.email,
      Name: this.state.firstName,
      Password: this.state.password,
    });

    if (successful === true) {
      // await this.props.bankAdminRegistration(this.state.firstName);
      // await this.props.editAdmin({
      //   AdminId: this.props.admin.id,
      //   MerchantId: this.props.bankAdmin.merchantId,
      // });
      // await this.props.pspAdminRegistration({
      //   MerchantId: this.props.bankAdmin.merchantId,
      //   MerchantPassword: this.props.bankAdmin.merchantPassword,
      //   Name: this.state.firstName,
      //   RegisteredWebShopId: "12345678-1234-1234-1234-123412341230",
      // });
      window.location = "/login";
    } else {
      toast.configure();
      toast.error("Unsuccessful registration!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }
}

const mapStateToProps = (state) => ({
  admin: state.admin,
  bankAdmin: state.bankAdmin,
});

export default compose(
  connect(mapStateToProps, {
    adminRegistration,
    bankAdminRegistration,
    editAdmin,
    pspAdminRegistration,
  })
)(AdminRegistration);
