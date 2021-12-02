import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import DatePicker from "react-datepicker/dist/react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import moment from "moment";

class BuyAccommodationModal extends Component {
  state = {
    showBuyModal: this.props.show,
    item: this.props.item,
    quantity: 0,
    startDate: "",
    endDate: "",
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
          Buy accommodation
        </ModalHeader>
        <ModalBody>
          <div>
            <label> Quantity </label>
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
          <div>
            <label> Date From: </label>
            <DatePicker
              className="form-control w-100"
              id="startDate"
              name="startDate"
              dateFormat="dd/MM/yyyy"
              selected={this.state.startDate}
              minDate={new Date()}
              onChange={(e) => this.handleChangeStartDate(e)}
            />
          </div>
          <div>
            <label> Date To: </label>
            <DatePicker
              className="form-control w-100"
              id="endDate"
              name="endDate"
              dateFormat="dd/MM/yyyy"
              selected={this.state.endDate}
              minDate={new Date()}
              onChange={(e) => this.handleChangeEndDate(e)}
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            disabled={
              this.state.quantity <= 0 ||
              this.state.startDate === "" ||
              this.state.endDate === "" ||
              this.state.endDate < this.state.startDate
            }
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
    var item = this.state.item;
    item.name =
      item.name +
      " " +
      moment(this.state.startDate).format("DD/MM/YYYY") +
      "-" +
      moment(this.state.endDate).format("DD/MM/YYYY");
    debugger;
    const product = {
      item: item,
      quantity: this.state.quantity,
      type: "accommodation",
    };
    if (
      shoppingCartList.some((shoppingProduct) => {
        if (shoppingProduct.type === "accommodation") {
          if (product.item.id === shoppingProduct.item.id) {
            return shoppingProduct;
          }
        }
      })
    ) {
      var foundProduct = shoppingCartList.filter((request) => {
        if (request.type === "accommodation") {
          if (product.item.id === request.item.id) {
            return request;
          }
        }
      });
      const newQuantity =
        parseInt(foundProduct[0].quantity) + parseInt(product.quantity);
      const elementsIndex = shoppingCartList.findIndex((element) => {
        if (element.type === "accommodation") {
          if (element.item.id == product.item.id) {
            return element;
          }
        }
      });
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

  handleChangeStartDate = (e) => {
    this.setState({
      startDate: e,
    });
  };

  handleChangeEndDate = (e) => {
    this.setState({
      endDate: e,
    });
  };

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

export default connect(mapStateToProps, {})(BuyAccommodationModal);
