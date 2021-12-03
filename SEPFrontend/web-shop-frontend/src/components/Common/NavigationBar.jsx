import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import {
  NavbarBrand,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
} from "reactstrap";

class NavigationBar extends Component {
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
            to="/item"
            onClick={() => {
              window.location = "/items";
            }}
          >
            <img src="/images/boxes.png" />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/conferences"
            onClick={() => {
              window.location = "/conferences";
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
            to="/courses"
            onClick={() => {
              window.location = "/courses";
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
            to="/accommodations-for-city"
            onClick={() => {
              window.location = "/accommodations-for-city";
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
            to="/transportations"
            onClick={() => {
              window.location = "/transportations";
            }}
          >
            <img
              src="/images/transportation.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/items-in-shopping-cart"
            onClick={() => {
              window.location = "/items-in-shopping-cart";
            }}
          >
            <img
              src="/images/shopping-cart.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/buyers-transactions"
            onClick={() => {
              window.location = "/buyers-transactions";
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

export default NavigationBar;
