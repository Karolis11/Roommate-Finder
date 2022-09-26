import { Component } from 'react';
import React from 'react';
import { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import CustomRoommates from './CustomRoommates'
import "yup-phone";
import { Slider } from '@material-ui/core'

const options = [
    { value: '1', label: '1' },
    { value: '2', label: '2' },
    { value: '3', label: '3+' },
]

export const CreateListingComponent = (props) => {

    const [value, setValue] = useState(100); // slider settings

    const changeValue = (event, value) => {
        setValue(value);
    }

    const customMarks = [
        {
            value: 100,
            label: '100EU'
        },

        {
            value: 500,
            label: '500EU'
        },

        {
            value: 1000,
            label: '1000EU'
        }
    ]

    const getText = (value) => '${value}'

    const [responseMessage, setResponseMessage] = useState("");

    const formik = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            city: "",
            phone: "",
            roommateCount: "1",
            maxPrice: '',
            extraComment: "",
        },
        validationSchema: Yup.object({
            firstName: Yup
                .string()
                .max(20, "Name can only be up to 20 characters")
                .required("Required"),
            lastName: Yup
                .string()
                .max(20, "Lastname can only be up to 20 characters")
                .required("Required"),
            phone: Yup
                .string()
                .phone("LT")
                .required("Required"),
            email: Yup
                .string()
                .email("Invalid email address")
                .required("Required"),
            city: Yup
                .string()
                .max(30, "City can only be up to 30 characters")
                .required("Required"),
            extraComment: Yup
                .string()
                .max(200, "Extra Comment can only be up to 200 characters")
        }),
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/createlisting',
                data: values
            }).then((response) => {
                console.log(response.data);
                props.toggleCreateListing(false);
            })
            }
        }
    )

    return (
        <>
            <form onSubmit={formik.handleSubmit}>
                <table>
                    <tbody>
                        <tr>
                            <td><label htmlFor="firstName">First name</label></td>
                            <td>
                                <input
                                    name="firstName"
                                    id="firstName"
                                    type="text"
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    value={formik.values.firstName}
                                />
                                {
                                    formik.touched.firstName && formik.errors.firstName
                                        ?
                                        <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                            {formik.errors.firstName}
                                        </p>
                                        : null
                                }
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="lastName">Last name</label></td>
                            <td>
                                <input
                                    name="lastName"
                                    id="lastName"
                                    type="text"
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    value={formik.values.lastName}
                                />
                                {
                                    formik.touched.lastName && formik.errors.lastName
                                        ?
                                        <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                            {formik.errors.lastName}
                                        </p>
                                        : null
                                }
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="email">Email</label></td>
                            <td>
                                <input
                                    name="email"
                                    id="email"
                                    type="email"
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    value={formik.values.email}
                                />
                                {
                                    formik.touched.email && formik.errors.email
                                        ?
                                        <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                            {formik.errors.email}
                                        </p>
                                        : null
                                }
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="phone">Phone Number</label></td>
                            <td>
                                <input
                                    name="phone"
                                    id="phone"
                                    type="phone"
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    value={formik.values.phone}
                                />
                                {
                                    formik.touched.phone && formik.errors.phone
                                        ?
                                        <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                            {formik.errors.phone}
                                        </p>
                                        : null
                                }
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="city">City</label></td>
                            <td>
                                <input
                                    name="city"
                                    id="city"
                                    type="text"
                                    
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    value={formik.values.city}
                                />
                                {
                                    formik.touched.city && formik.errors.city
                                        ?
                                        <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                            {formik.errors.city}
                                        </p>
                                        : null
                                }
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="roomateCount">Number of Roommates</label></td>
                            <td>
                                <CustomRoommates
                                    options={options}
                                    value={formik.values.roommateCount}
                                    className={'number'}
                                    onChange={value => formik.setFieldValue('roommateCount',value.value)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="price">Price Range:</label></td>
                            <td height="150">
                                <Slider
                                    min={100}
                                    max={1000}
                                    defaultValue={100}
                                    step={10}
                                    marks={customMarks}
                                    getAriaValueText={getText}
                                    valueLabelDisplay='auto'
                                // add value assign later --
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="extraComment">Extra Comment * </label></td>
                            <td>
                                <input
                                    name="extraComment"
                                    id="extraComment"
                                    type="extraComment"
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    value={formik.values.extraComment}
                                />
                                {
                                    formik.touched.extraComment && formik.errors.extraComment
                                        ?
                                        <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                            {formik.errors.extraComment}
                                        </p>
                                        : null
                                }
                            </td>
                        </tr>
                    </tbody>
                </table>
                <button type="submit">Submit</button>
            </form>
            <small>   * - Optional Fields</small>
            <p>{responseMessage}</p>
        </>
    )
}
 