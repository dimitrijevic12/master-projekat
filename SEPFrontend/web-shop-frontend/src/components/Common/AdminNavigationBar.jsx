import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import {
  NavbarBrand,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
} from "reactstrap";

class AdminNavigationBar extends Component {
  state = {};

  render() {
    const NavBar = () => {
      return (
        <React.Fragment>
          <NavLink
            exact
            to="/"
            onClick={() => {
              window.location = "/";
            }}
          >
            <img src="/images/home.png" />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/owners-items"
            onClick={() => {
              window.location = "/owners-items";
            }}
          >
            <img src="/images/boxes.png" />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/owners-conferences"
            onClick={() => {
              window.location = "/owners-conferences";
            }}
          >
            <img
              src="/images/conference.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/owners-courses"
            onClick={() => {
              window.location = "/owners-courses";
            }}
          >
            <img
              src="/images/online-course.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/owners-accommodations"
            onClick={() => {
              window.location = "/owners-accommodations";
            }}
          >
            <img
              src="/images/accomodation.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/sellers-transactions"
            onClick={() => {
              window.location = "/sellers-transactions";
            }}
          >
            <img
              src="/images/transaction.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <UncontrolledDropdown style={{ float: "right" }}>
            <DropdownToggle nav caret></DropdownToggle>
            <DropdownMenu right>
              <DropdownItem>
                <NavLink to="/login" onClick={this.logout.bind(this)}>
                  Logout
                </NavLink>
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </React.Fragment>
      );
    };
    return <NavBar />;
  }

  logout() {
    this.removeLocalStorage();
    this.removeSessionStorage();
    this.props.history.push("/login");
  }

  removeLocalStorage() {
    localStorage.setItem("shoppingCart", "");
  }

  removeSessionStorage() {
    sessionStorage.setItem("tokenWebShop", "");
    sessionStorage.setItem("userIdWebShop", "");
    sessionStorage.setItem("roleWebShop", "");
    sessionStorage.setItem("usernameWebShop", "");
  }
}

export default AdminNavigationBar;
