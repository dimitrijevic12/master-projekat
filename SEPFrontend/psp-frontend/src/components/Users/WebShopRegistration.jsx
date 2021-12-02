import React, { Component } from "react";
import { webShopRegistration } from "../../actions/actionsWebShop";
import { connect } from "react-redux";
import { toast } from "react-toastify";
import { compose } from "redux";
import "react-toastify/dist/ReactToastify.css";

class WebShopRegistration extends Component {
  state = {
    webShopId: 1,
    webShopName: "",
    emailAddress: "",
    password: "",
    repeatPassword: "",
    successUrl: "",
    failedUrl: "",
    errorUrl: "",
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
                  <label for="webShopName">Web Shop Name:</label>
                  <input
                    type="text"
                    name="webShopName"
                    value={this.state.webShopName}
                    onChange={this.handleChange}
                    class="form-control"
                    id="webShopName"
                    placeholder="Enter web shop name"
                  />
                </div>
              </div>
            
           
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="emailAddress">Email Address:</label>
                  <input
                    type="text"
                    name="emailAddress"
                    value={this.state.emailAddress}
                    onChange={this.handleChange}
                    class="form-control"
                    id="emailAddress"
                    placeholder="Enter email address"
                  />
                </div>
              </div>
              </div>
              <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="password">Password:</label>
                  <input
                    type="text"
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
              <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="successUrl">Success Url:</label>
                  <input
                    type="successUrl"
                    name="successUrl"
                    value={this.state.successUrl}
                    onChange={this.handleChange}
                    class="form-control"
                    id="successUrl"
                    placeholder="Enter Success Url"
                  />
                </div>
              </div>
            
           
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="failedUrl">Failed Url:</label>
                  <input
                    type="failedUrl"
                    name="failedUrl"
                    value={this.state.failedUrl}
                    onChange={this.handleChange}
                    class="form-control"
                    id="failedUrl"
                    placeholder="Enter Failed Url"
                  />
                </div>
              </div>
              </div>
              <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="errorUrl">Error Url:</label>
                  <input
                    type="errorUrl"
                    name="errorUrl"
                    value={this.state.errorUrl}
                    onChange={this.handleChange}
                    class="form-control"
                    id="errorUrl"
                    placeholder="Enter Error Url"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button
                disabled={this.state.webShopName === "" || this.state.emailAddress === "" || this.state.successUrl === "" 
                || this.state.errorUrl === "" || this.state.failedUrl === "" || this.state.password === ""
                 || this.state.repeatPassword === "" || this.state.repeatPassword != this.state.password}
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
    successful = await this.props.webShopRegistration({
      webShopId: 1,
      webShopName: this.state.webShopName,
      emailAddress: this.state.emailAddress,
      password: this.state.password,
      successUrl: this.state.successUrl,
      failedUrl: this.state.failedUrl,
      errorUrl: this.state.errorUrl
    });

    if (successful === true) {
      window.location = "/login";
    } else {
      toast.configure();
      toast.error("Unsuccessful registration!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }
}

const mapStateToProps = (state) => ({});

export default compose(
  connect(mapStateToProps, {
    webShopRegistration,
  })
)(WebShopRegistration);
