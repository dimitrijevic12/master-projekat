import React, { Component } from "react";
import "../css/app.css";
import { connect } from "react-redux";
import background from "../images/background-login.jpg";
import { userLoggedIn } from "../actions/actionsUsers";
import { compose } from "redux";
import Header from "../components/Common/Header";
import "bootstrap/dist/css/bootstrap.min.css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

class LoginPage extends Component {
  state = {
    username: "",
    password: "",
  };
  render() {
    return (
      <div
        style={{
          width: 1580,
          height: 1080,
          backgroundImage: `url(${background})`,
        }}
      >
        <Header />
        <div className="container"></div>
        <div id="wrapper">
          <div class="main-content">
            <div class="l-part">
              <input
                type="text"
                placeholder="Username"
                name="username"
                class="input-1"
                onChange={this.handleChange}
              />
              <div class="overlap-text">
                <input
                  type="password"
                  placeholder="Password"
                  name="password"
                  class="input-2"
                  onChange={this.handleChange}
                />
                <a href="#">Forgot?</a>
              </div>
              <input
                type="button"
                onClick={this.login.bind(this)}
                value="Log in"
                class="btn btn-primary"
              />
            </div>
          </div>
          <div class="sub-content">
            <div class="s-part">
              Don't have an account?
              <a onClick={this.register.bind(this)} href="javascript:;">
                Sign up
              </a>
            </div>
          </div>
        </div>
      </div>
    );
  }

  register() {
    window.location = "/registration";
  }

  async login() {
    debugger;
    var successful = false;
    successful = await this.props.userLoggedIn({
      Username: this.state.username,
      Password: this.state.password,
    });

    if (successful === true) {
      if (sessionStorage.getItem("roleWebShop") === "RegisteredUser") {
        window.location = "/items";
      } else {
        window.location = "/owners-items";
      }
    } else {
      toast.configure();
      toast.error("Unsuccessful login!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
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

const mapStateToProps = (state) => ({});

export default compose(
  connect(mapStateToProps, {
    userLoggedIn,
  })
)(LoginPage);
