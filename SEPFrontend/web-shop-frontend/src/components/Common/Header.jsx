import React, { Component } from "react";
import NotLoggedNavigationBar from "./NavigationBar";

class Header extends Component {
  state = {};
  render() {
    return (
      <header style={{ backgroundColor: "whitesmoke" }} className="pt-4">
        Slika
        <span style={{ width: 200, display: "inline-block" }}></span>
        <span style={{ width: 300, display: "inline-block" }}>Search</span>
        <NotLoggedNavigationBar />
        <hr />
      </header>
    );
  }
}

export default Header;
