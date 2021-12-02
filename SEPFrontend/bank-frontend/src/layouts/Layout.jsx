import React, { Component } from "react";

class Layout extends Component {
  componentDidMount() {
    document.title = "Bank";
  }

  render() {
    return (
      <div className="w-100">
        <div className="">{this.props.children}</div>
      </div>
    );
  }
}

export default Layout;
