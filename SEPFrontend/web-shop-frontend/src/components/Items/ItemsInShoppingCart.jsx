import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getItems, getImagesForItems } from "../../actions/actionsItems";
import { saveTransaction } from "../../actions/actionsTransaction";

class ItemsInShoppingCart extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
    itemsInShoppingCart: [],
  };
  async componentDidMount() {
    debugger;
    await this.getItemsFromShoppingCart();
    await this.getAllImages(this.state.itemsInShoppingCart);
  }
  render() {
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    if (this.props.itemsImages === undefined) {
      return null;
    }
    debugger;
    const ItemsList = () => {
      debugger;
      return shoppingCartList.map((post, i) =>
        this.props.itemsImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={
              "data:image/jpg;base64," + this.props.itemsImages[i].fileContents
            }
            onClick={() => this.view(post)}
          />
        ) : (
          <video
            controls
            onClick={() => this.view(post)}
            style={{ width: 367, height: 370 }}
            className="mb-3"
          >
            <source
              src={
                "data:video/mp4;base64," +
                this.props.itemsImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    };

    return shoppingCartList.length === 0 ? (
      <div className="text-center pt-5">
        <img src="/images/empty-shopping-cart.png" />
        <br />
        <h4>Shopping cart is empty!</h4>
      </div>
    ) : (
      <div>
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>
              Items In Shopping Cart
            </GridControlBarItem>
          </GridControlBar>
          <button
            onClick={() => this.removeFromShoppingCart()}
            className="btn btn-primary"
          >
            Remove everything from shopping cart
          </button>
          <hr />
          <br />
          <ItemsList />
          <br />
          <button onClick={() => this.buy()} className="btn btn-primary">
            Buy, Total price: {this.getTotalPrice()}€
          </button>
        </Grid>
      </div>
    );
  }

  async buy() {
    debugger;
    var transactionItems = [];
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    const items = this.state.itemsInShoppingCart;
    for (var i = 0; i < shoppingCartList.length; i++) {
      var type = 0;
      var productId = 0;
      if (shoppingCartList[i].type === "item") {
        type = 4;
        productId = items[i].productKey;
      } else if (shoppingCartList[i].type === "conference") {
        type = 0;
        productId = items[i].id;
      } else if (shoppingCartList[i].type === "course") {
        type = 1;
        productId = items[i].id;
      } else if (shoppingCartList[i].type === "accommodation") {
        type = 2;
        productId = items[i].id;
      }
      if (shoppingCartList[i].type === "accommodation") {
        var transactionItem = {
          Type: type,
          ProductId: productId,
          Name: items[i].name,
          Quantity: shoppingCartList[i].quantity,
          Price: items[i].costPerNight * shoppingCartList[i].quantity,
        };
      } else {
        var transactionItem = {
          Type: type,
          ProductId: productId,
          Name: items[i].name,
          Quantity: shoppingCartList[i].quantity,
          Price: items[i].price * shoppingCartList[i].quantity,
        };
      }

      transactionItems.push(transactionItem);
    }
    const transaction = {
      Status: 0,
      TimeStamp: new Date().toJSON(),
      TotalPrice: this.getTotalPrice(),
      SellerId: shoppingCartList[0].item.ownerId,
      BuyerId: sessionStorage.getItem("userIdWebShop"),
      TransactionItems: transactionItems,
    };
    debugger;
    await this.props.saveTransaction(transaction);
    localStorage.setItem("shoppingCart", "");
    window.location = "/items-in-shopping-cart";
  }

  getTotalPrice() {
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    var totalPrice = 0;
    const items = this.state.itemsInShoppingCart;
    for (var i = 0; i < shoppingCartList.length; i++) {
      debugger;
      if (shoppingCartList[i].type === "accommodation") {
        totalPrice +=
          items[i].costPerNight *
          shoppingCartList[i].quantity *
          this.getDaysBetween(items[i].name);
      } else {
        totalPrice += items[i].price * shoppingCartList[i].quantity;
      }
    }
    return totalPrice;
  }

  getDaysBetween = (name) => {
    debugger;
    const endDate = name.substring(name.length - 10, name.length);
    const startDate = name.substring(name.length - 21, name.length - 11);
    const date1 = this.parseDate(startDate);
    const date2 = this.parseDate(endDate);
    return this.dateDiff(date1, date2);
  };

  parseDate = (str) => {
    var mdy = str.split("/");
    return new Date(+mdy[2], mdy[1] - 1, +mdy[0]);
  };

  dateDiff = (first, second) => {
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
  };

  removeFromShoppingCart() {
    localStorage.setItem("shoppingCart", "");
    window.location = "/items-in-shopping-cart";
  }

  view(f) {
    if (f.type === "item") {
      localStorage.setItem("shoppingItem-id", f.item.productKey);
      localStorage.setItem("quantity-for-shopping-item", f.quantity);
      window.location = "/shopping-item/" + f.item.productKey;
    } else if (f.type === "conference") {
      localStorage.setItem("shoppingItem-id", f.item.id);
      localStorage.setItem("quantity-for-shopping-item", f.quantity);
      window.location = "/shopping-conference/" + f.item.id;
    } else if (f.type === "course") {
      localStorage.setItem("shoppingItem-id", f.item.id);
      localStorage.setItem("quantity-for-shopping-item", f.quantity);
      window.location = "/shopping-course/" + f.item.id;
    } else if (f.type === "accommodation") {
      debugger;
      localStorage.setItem("shoppingAccommodation-id", f.item.id);
      localStorage.setItem("quantity-for-shopping-item", f.quantity);
      localStorage.setItem("shopping-accommodation-name", f.item.name);
      window.location = "/shopping-accommodation/" + f.item.id;
    }
  }

  getAllImages = async (items) => {
    debugger;
    const paths = [];
    for (var i = 0; i < items.length; i++) {
      if (items[i].imagePath === undefined) {
        paths.push(items[i].imagePaths[0]);
      } else {
        paths.push(items[i].imagePath);
      }
    }
    await this.props.getImagesForItems(paths);
  };

  getItemsFromShoppingCart = async () => {
    debugger;
    const items = [];
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    for (var i = 0; i < shoppingCartList.length; i++) {
      items.push(shoppingCartList[i].item);
    }
    this.setState({
      itemsInShoppingCart: items,
    });
  };
}

const mapStateToProps = (state) => ({
  items: state.items,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getItems,
    getImagesForItems,
    saveTransaction,
  })
)(ItemsInShoppingCart);
