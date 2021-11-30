import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import { getItemById, editItem } from "../../actions/actionsItems";

class EditItem extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    price: "",
    availableCount: "",
    description: "",
  };

  async componentDidMount() {
    await this.props.getItemById(localStorage.getItem("item-productkey"));
    debugger;
    this.setState({
      name: this.props.item.name,
      price: this.props.item.price,
      description: this.props.item.description,
    });
  }

  render() {
    if (this.props.item === undefined) {
      return null;
    }

    if (this.props.loadedImage === undefined) {
      return null;
    }
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <img
                src={
                  "data:image/jpg;base64," + this.props.loadedImage.fileContents
                }
                style={{ width: 600, height: 320 }}
                className="mb-3"
              />
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label>You can choose new photo if you want to: </label>
              <input type="file" onChange={this.choosePost} />
              <img
                src={this.state.fileUrl}
                style={{ width: 500, height: 300 }}
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label className="label">Name:</label>
              <input
                type="text"
                name="name"
                value={this.state.name}
                onChange={this.handleChange}
                class="form-control"
                id="name"
                placeholder="Enter name of product"
              />
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="tag">Price:</label>
              <input
                type="number"
                name="price"
                value={this.state.price}
                onChange={this.handleChange}
                class="form-control"
                id="price"
                placeholder="Enter price"
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="location">Description:</label>
              <textarea
                name="description"
                value={this.state.description}
                cols="40"
                rows="5"
                class="form-control"
                onChange={this.handleChange}
                placeholder="Enter description"
              ></textarea>
            </div>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            onClick={() => {
              this.createItem();
            }}
            disabled={this.state.name === "" || this.state.price === ""}
            className="btn btn-primary btn-block"
          >
            Edit
          </button>
        </div>
      </React.Fragment>
    );
  }

  async createItem() {
    debugger;
    const newItem = {
      productKey: this.props.item.productKey,
      name: this.state.name,
      imagePath:
        this.state.contentPath === ""
          ? this.props.item.imagePath
          : this.state.contentPath,
      price: this.state.price,
      description: this.state.description,
      ownerId: this.props.item.ownerId,
    };
    await this.props.editItem({
      ProductKey: this.props.item.productKey,
      Name: this.state.name,
      ImagePath:
        this.state.contentPath === ""
          ? this.props.item.imagePath
          : this.state.contentPath,
      Price: this.state.price,
      Description: this.state.description,
      OwnerId: this.props.item.ownerId,
    });
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    if (
      shoppingCartList.some((shoppingProduct) => {
        if (shoppingProduct.type === "item") {
          if (newItem.productKey === shoppingProduct.item.productKey) {
            return shoppingProduct;
          }
        }
      })
    ) {
      const elementsIndex = shoppingCartList.findIndex((element) => {
        if (element.type === "item") {
          if (element.item.productKey === newItem.productKey) {
            return element;
          }
        }
      });
      let newArray = shoppingCartList;
      newArray[elementsIndex] = {
        ...newArray[elementsIndex],
        item: newItem,
      };
      localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    }
    window.location = "/items";
  }

  choosePost = async (event) => {
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
      fileType: event.target.files[0].type,
    });
    const formData = new FormData();

    formData.append("formFile", event.target.files[0]);
    formData.append("fileName", event.target.files[0].name);

    var dummyThis = this;
    try {
      const res = await axios({
        method: "post",
        url: "https://localhost:5001/api/contents",
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
        .then(function (response) {
          var contentPath = { ...dummyThis.state.contentPath };
          contentPath = response.data;
          dummyThis.setState({ contentPath });
          console.log(response);
        })
        .catch(function (response) {
          console.log(response);
        });
      console.log(res);
      debugger;
      console.log(this.state.contentPath);
    } catch (ex) {
      console.log(ex);
    }
  };

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };
}

const mapStateToProps = (state) => ({
  item: state.item,
  loadedImage: state.loadedImage,
});

export default compose(connect(mapStateToProps, { getItemById, editItem }))(
  EditItem
);
