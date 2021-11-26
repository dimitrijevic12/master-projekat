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
          <button
            //onClick={() => this.removeFromShoppingCart()}
            className="btn btn-primary"
          >
            Buy
          </button>
        </Grid>
      </div>
    );
  }

  removeFromShoppingCart() {
    localStorage.setItem("shoppingCart", "");
    window.location = "/items-in-shopping-cart";
  }

  view(f) {
    localStorage.setItem("shoppingItem-productkey", f.item.productKey);
    localStorage.setItem("quantity-for-shopping-item", f.quantity);
    window.location = "/shopping-item/" + f.item.productKey;
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
  })
)(ItemsInShoppingCart);
