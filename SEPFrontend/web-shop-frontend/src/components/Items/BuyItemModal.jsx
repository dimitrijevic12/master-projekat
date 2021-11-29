import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";

class BuyItemModal extends Component {
  state = {
    showBuyModal: this.props.show,
    item: this.props.item,
    quantity: 0,
  };

  render() {
    debugger;
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showBuyModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          Enter quantity
        </ModalHeader>
        <ModalBody>
          <div>
            <input
              type="number"
              name="quantity"
              value={this.state.quantity}
              onChange={this.handleChange}
              class="form-control"
              id="quantity"
              placeholder="Enter quantity you want to buy"
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            disabled={this.state.quantity <= 0}
            className="btn btn-primary"
            onClick={() => {
              this.buy();
            }}
          >
            Add to cart
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  buy() {
    debugger;
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    const product = { item: this.state.item, quantity: this.state.quantity };
    if (
      shoppingCartList.some(
        (shoppingProduct) =>
          product.item.productKey === shoppingProduct.item.productKey
      )
    ) {
      var foundProduct = shoppingCartList.filter(
        (request) => product.item.productKey === request.item.productKey
      );
      const newQuantity =
        parseInt(foundProduct[0].quantity) + parseInt(product.quantity);
      const elementsIndex = shoppingCartList.findIndex(
        (element) => element.item.productKey == product.item.productKey
      );
      let newArray = shoppingCartList;
      newArray[elementsIndex] = {
        ...newArray[elementsIndex],
        quantity: newQuantity,
      };
    } else {
      shoppingCartList = shoppingCartList.concat(product);
    }
    debugger;
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    this.toggle();
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

  toggle() {
    debugger;
    this.setState({ showBuyModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, {})(BuyItemModal);
