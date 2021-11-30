import React, { useEffect, useState } from "react";
import { getItemById } from "../../actions/actionsItems";
import { connect } from "react-redux";
import { compose } from "redux";
import BuyItemModal from "./BuyItemModal";

function ReviewItemInShoppingCart(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getItemById(localStorage.getItem("shoppingItem-id"));
  }, []);

  const Item = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    debugger;
    return (
      <img
        src={"data:image/jpg;base64," + props.loadedImage.fileContents}
        style={{ width: 600, height: 320 }}
        className="mb-3"
      />
    );
  };

  const displayModalPost = (item) => {
    if (item != undefined) {
      setItem(item);
    }
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    shoppingCartList = shoppingCartList.filter((cartItem) => {
      if (cartItem.type === "item") {
        return cartItem.item.productKey !== item.productKey;
      } else {
        return cartItem;
      }
    });
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    window.location = "/items-in-shopping-cart";
  };

  if (props.item === undefined) {
    return null;
  }

  return (
    <React.Fragment>
      {showPostModal ? (
        <BuyItemModal
          show={showPostModal}
          item={item}
          onShowChange={() => displayModalPost()}
        />
      ) : null}
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <Item />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label className="label">Name:</label>
            <input
              type="text"
              name="name"
              class="form-control"
              id="name"
              value={props.item.name}
              disabled={true}
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
              type="text"
              name="price"
              class="form-control"
              id="price"
              value={props.item.price + " €"}
              disabled={true}
              placeholder="Enter price"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Description:</label>
            <textarea
              name="promotedAction"
              value={props.item.description}
              cols="40"
              rows="5"
              class="form-control"
              disabled={true}
              placeholder="Enter promotion/action"
            ></textarea>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Quantity:</label>
            <input
              type="number"
              name="quantity"
              class="form-control"
              id="quantity"
              value={localStorage.getItem("quantity-for-shopping-item")}
              disabled={true}
              placeholder="Enter quantity"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Total price:</label>
            <input
              type="text"
              name="quantity"
              class="form-control"
              id="quantity"
              value={
                parseInt(localStorage.getItem("quantity-for-shopping-item")) *
                  parseInt(props.item.price) +
                " €"
              }
              disabled={true}
              placeholder="Enter quantity"
            />
          </div>
        </div>
      </div>
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button
          onClick={() => displayModalPost(props.item)}
          className="btn btn-primary"
        >
          Remove item from cart
        </button>
      </div>
    </React.Fragment>
  );
}

const mapStateToProps = (state) => ({
  item: state.item,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, {
    getItemById,
  })
)(ReviewItemInShoppingCart);
