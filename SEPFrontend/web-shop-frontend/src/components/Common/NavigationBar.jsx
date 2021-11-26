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
            to={"/profile/" + sessionStorage.getItem("userId")}
            onClick={() => {
              window.location = "/profile/" + sessionStorage.getItem("userId");
            }}
          >
            <img
              src="/images/user.png"
              style={{ width: 24, height: 24, borderRadius: 50 }}
            />
          </NavLink>
          <UncontrolledDropdown style={{ float: "right" }}>
            <DropdownToggle nav caret></DropdownToggle>
            <DropdownMenu right>
              <DropdownItem>
                <NavLink
                  to="/edit"
                  onClick={() => {
                    window.location = "/edit";
                  }}
                >
                  Edit profile
                </NavLink>
              </DropdownItem>
              <DropdownItem divider />
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

  logout() {}
}

export default NavigationBar;
