import React, { Component } from "react";
import NavigationBar from "./NavigationBar";
import NotLoggedNavigationBar from "./NotLoggedNavigationBar";
import AdminNavigationBar from "./AdminNavigationBar";
import HeadOfAcquirementNavigationBar from "./HeadOfAcquirementNavigationBar";
import StaffNavigationBar from "./StaffNavigationBar";
import SearchBar from "./SearchBar";

class Header extends Component {
  state = {};
  render() {
    debugger;
    return (
      <header style={{ backgroundColor: "whitesmoke" }} className="pt-4">
        <span style={{ width: 30, display: "inline-block" }}></span>
        <label style={{ fontWeight: "bold" }}>WebShop</label>
        <span style={{ width: 500, display: "inline-block" }}></span>
        <span style={{ width: 300, display: "inline-block" }}>
          <SearchBar></SearchBar>
        </span>
        {sessionStorage.getItem("tokenWebShop") === "" ? (
          <span
            style={{ display: "inline-block", float: "right", zIndex: "4" }}
          >
            <NotLoggedNavigationBar />
          </span>
        ) : sessionStorage.getItem("roleWebShop") === "Admin" ? (
          <span
            style={{ display: "inline-block", float: "right", zIndex: "4" }}
          >
            <AdminNavigationBar />
          </span>
        ) : sessionStorage.getItem("itRoleWebShop") === "default" ? (
          <span
            style={{ display: "inline-block", float: "right", zIndex: "4" }}
          >
            <NavigationBar />
          </span>
        ) : sessionStorage.getItem("itRoleWebShop") === "headOfAcquirement" ? (
          <span
            style={{ display: "inline-block", float: "right", zIndex: "4" }}
          >
            <HeadOfAcquirementNavigationBar />
          </span>
        ) : (
          <span
            style={{ display: "inline-block", float: "right", zIndex: "4" }}
          >
            <StaffNavigationBar />
          </span>
        )}
        <hr />
      </header>
    );
  }
}

export default Header;
