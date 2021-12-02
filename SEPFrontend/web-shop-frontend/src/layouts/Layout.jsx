import React, { Component } from "react";
import Header from "../components/Common/Header";
import background from "../images/background-login.jpg";

class Layout extends Component {
  componentDidMount() {
    document.title = "Web Shop";
  }

  render() {
    return (
      <React.Fragment>
        <Header />
        <div className="container">
          <div className="wrapper">{this.props.children}</div>
        </div>
      </React.Fragment>
    );
  }
}

export default Layout;
