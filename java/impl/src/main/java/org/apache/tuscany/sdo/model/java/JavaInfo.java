/**
 *
 *  Licensed to the Apache Software Foundation (ASF) under one
 *  or more contributor license agreements.  See the NOTICE file
 *  distributed with this work for additional information
 *  regarding copyright ownership.  The ASF licenses this file
 *  to you under the Apache License, Version 2.0 (the
 *  "License"); you may not use this file except in compliance
 *  with the License.  You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing,
 *  software distributed under the License is distributed on an
 *  "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 *  KIND, either express or implied.  See the License for the
 *  specific language governing permissions and limitations
 *  under the License.
 */
package org.apache.tuscany.sdo.model.java;

import java.io.Serializable;

/**
 * <!-- begin-user-doc -->
 * A representation of the model object '<em><b>Info</b></em>'.
 * <!-- end-user-doc -->
 *
 * <p>
 * The following features are supported:
 * <ul>
 *   <li>{@link org.apache.tuscany.sdo.model.java.JavaInfo#getJavaClass <em>Java Class</em>}</li>
 * </ul>
 * </p>
 *
 * @extends Serializable
 * @generated
 */
public interface JavaInfo extends Serializable
{
  /**
   * Returns the value of the '<em><b>Java Class</b></em>' attribute.
   * <!-- begin-user-doc -->
   * <p>
   * If the meaning of the '<em>Java Class</em>' attribute isn't clear,
   * there really should be more of a description here...
   * </p>
   * <!-- end-user-doc -->
   * @return the value of the '<em>Java Class</em>' attribute.
   * @see #isSetJavaClass()
   * @see #unsetJavaClass()
   * @see #setJavaClass(String)
   * @generated
   */
  String getJavaClass();

  /**
   * Sets the value of the '{@link org.apache.tuscany.sdo.model.java.JavaInfo#getJavaClass <em>Java Class</em>}' attribute.
   * <!-- begin-user-doc -->
   * <!-- end-user-doc -->
   * @param value the new value of the '<em>Java Class</em>' attribute.
   * @see #isSetJavaClass()
   * @see #unsetJavaClass()
   * @see #getJavaClass()
   * @generated
   */
  void setJavaClass(String value);

  /**
   * Unsets the value of the '{@link org.apache.tuscany.sdo.model.java.JavaInfo#getJavaClass <em>Java Class</em>}' attribute.
   * <!-- begin-user-doc -->
   * <!-- end-user-doc -->
   * @see #isSetJavaClass()
   * @see #getJavaClass()
   * @see #setJavaClass(String)
   * @generated
   */
  void unsetJavaClass();

  /**
   * Returns whether the value of the '{@link org.apache.tuscany.sdo.model.java.JavaInfo#getJavaClass <em>Java Class</em>}' attribute is set.
   * <!-- begin-user-doc -->
   * <!-- end-user-doc -->
   * @return whether the value of the '<em>Java Class</em>' attribute is set.
   * @see #unsetJavaClass()
   * @see #getJavaClass()
   * @see #setJavaClass(String)
   * @generated
   */
  boolean isSetJavaClass();

} // JavaInfo
