import React, { Component } from "react";
import NotLoggedNavigationBar from "./NavigationBar";

class Header extends Component {
  state = {};
  render() {
    return (
      <header style={{ backgroundColor: "whitesmoke" }} className="pt-4">
        
        <NotLoggedNavigationBar />
        <hr />
      </header>
    );
  }
}

export default Header;