import React, { useEffect, useState } from "react";
import { getItemById } from "../../actions/actionsItems";
import { connect } from "react-redux";
import { compose } from "redux";
import BuyItemModal from "./BuyItemModal";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function ReviewItem(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getItemById(localStorage.getItem("item-productkey"));
  }, []);

  const Item = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    debugger;
    console.log(sessionStorage.getItem("userIdAgentApp"));
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
    debugger;
    if (item !== undefined) {
      if (shoppingCartList.length > 0) {
        if (shoppingCartList[0].item.ownerId != item.ownerId) {
          toast.configure();
          toast.error("You can't add items of another owner to cart!", {
            position: toast.POSITION.TOP_RIGHT,
          });
        } else {
          setShowPostModal(!showPostModal);
        }
      }
    }
    debugger;
  };

  const edit = (item) => {
    localStorage.setItem("item-productkey", item.productKey);
    window.location = "/edit-item";
  };

  // const deleteItem = async () => {
  //   await props.deleteItem(props.item);
  //   window.location = "/";
  // };

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
              name="description"
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
      {/* {sessionStorage.getItem("userIdAgentApp") === undefined ||
      sessionStorage.getItem("userIdAgentApp") === "" ? (
        ""
      ) : sessionStorage.getItem("roleAgentApp") === "Agent" ? (
        <div style={{ textAlign: "center" }} className="mt-5 pb-5">
          <button
            onClick={() => {
              editItem();
            }}
            className="btn btn-danger"
          >
            Edit
          </button>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <button
            onClick={() => {
              deleteItem();
            }}
            className="btn btn-danger"
          >
            Delete
          </button>
        </div>
      ) : (
        <div style={{ textAlign: "center" }} className="mt-5 pb-5">
          <button
            onClick={() => displayModalPost(props.item)}
            className="btn btn-primary"
          >
            Buy
          </button>
        </div>
      )} */}
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button
          onClick={() => displayModalPost(props.item)}
          className="btn btn-primary"
        >
          Add to cart
        </button>
      </div>
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button onClick={() => edit(props.item)} className="btn btn-primary">
          Edit item
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
)(ReviewItem);
