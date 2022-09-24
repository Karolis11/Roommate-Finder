import React, { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';

export const Signup = (props) => {

    const [responseMessage, setResponseMessage] = useState("");

    const formik = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            city: ""
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
            email: Yup
                        .string()
                        .email("Invalid email address")
                        .required("Required"),
            city: Yup
                        .string()
                        .max(30, "City can only be up to 30 characters")
                        .required("Required")
        }),
        onSubmit: (values) => {
            
            setResponseMessage("Success")
            setTimeout(() => {
                props.toggleSignUp(false);
            }, 5000);
        }
    })

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
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
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
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
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
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.email}
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
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.city}
                                    </p> 
                                : null 
                            }
                        </td>
                    </tr>
                </tbody>
            </table>
            <button type="submit">Submit</button>
        </form>
        <p>{responseMessage}</p>
        </>
    )
}