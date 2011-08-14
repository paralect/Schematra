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
package org.apache.tuscany.sdo.util.resource;

import java.util.NoSuchElementException;

import javax.xml.namespace.NamespaceContext;
import javax.xml.namespace.QName;
import javax.xml.stream.Location;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamReader;

/**
 * This class is derived from Apache Axis2 class <a
 * href="http://svn.apache.org/repos/asf/webservices/axis2/trunk/java/modules/core/src/org/apache/axis2/util/StreamWrapper.java">
 * org.apache.axis2.util.StreamWrapper</a>. It's used wrap a XMLStreamReader to create a XMLStreamReader representing a document and it will produce
 * START_DOCUMENT, END_DOCUMENT events.
 * 
 */
public class XMLDocumentStreamReader implements XMLStreamReader {
    private static final int STATE_INIT = 0; // The wrapper will produce START_DOCUMENT

    private static final int STATE_SWITCHED = 1; // The real reader will produce events

    private static final int STATE_COMPLETE_AT_NEXT = 2; // The wrapper will produce END_DOCUMENT

    private static final int STATE_COMPLETED = 3; // Done

    private XMLStreamReader realReader = null;

    private int state = STATE_INIT;

    public XMLDocumentStreamReader(XMLStreamReader realReader) {
        if (realReader == null) {
            throw new UnsupportedOperationException("Reader cannot be null");
        }

        this.realReader = realReader;

        // If the real reader is positioned at START_DOCUMENT, always use the real reader
        if (realReader.getEventType() == START_DOCUMENT)
            state = STATE_SWITCHED;
    }

    public void close() throws XMLStreamException {
        realReader.close();
    }

    public int next() throws XMLStreamException {
        int returnEvent = -1;

        switch (state) {
        case STATE_SWITCHED:
            returnEvent = realReader.next();
            if (returnEvent == END_DOCUMENT) {
                state = STATE_COMPLETED;
            } else if (!realReader.hasNext()) {
                state = STATE_COMPLETE_AT_NEXT;
            }
            break;
        case STATE_INIT:
            state = STATE_SWITCHED;
            returnEvent = realReader.getEventType();
            break;
        case STATE_COMPLETE_AT_NEXT:
            state = STATE_COMPLETED;
            returnEvent = END_DOCUMENT;
            break;
        case STATE_COMPLETED:
            // oops - no way we can go beyond this
            throw new NoSuchElementException("End of stream has reached.");
        default:
            throw new UnsupportedOperationException();
        }

        return returnEvent;
    }

    private boolean isDelegating() {
        return state == STATE_SWITCHED || state == STATE_COMPLETE_AT_NEXT;
    }

    public int nextTag() throws XMLStreamException {
        if (isDelegating()) {
            return realReader.nextTag();
        } else {
            throw new XMLStreamException();
        }
    }

    public void require(int i, String s, String s1) throws XMLStreamException {
        if (isDelegating()) {
            realReader.require(i, s, s1);
        }
    }

    public boolean standaloneSet() {
        if (isDelegating()) {
            return realReader.standaloneSet();
        } else {
            return false;
        }
    }

    public int getAttributeCount() {
        if (isDelegating()) {
            return realReader.getAttributeCount();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getAttributeLocalName(int i) {
        if (isDelegating()) {
            return realReader.getAttributeLocalName(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public QName getAttributeName(int i) {
        if (isDelegating()) {
            return realReader.getAttributeName(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getAttributeNamespace(int i) {
        if (isDelegating()) {
            return realReader.getAttributeNamespace(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getAttributePrefix(int i) {
        if (isDelegating()) {
            return realReader.getAttributePrefix(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getAttributeType(int i) {
        if (isDelegating()) {
            return realReader.getAttributeType(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getAttributeValue(int i) {
        if (isDelegating()) {
            return realReader.getAttributeValue(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getAttributeValue(String s, String s1) {
        if (isDelegating()) {
            return realReader.getAttributeValue(s, s1);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getCharacterEncodingScheme() {
        return realReader.getCharacterEncodingScheme();
    }

    public String getElementText() throws XMLStreamException {
        if (isDelegating()) {
            return realReader.getElementText();
        } else {
            throw new XMLStreamException();
        }
    }

    public String getEncoding() {
        return realReader.getEncoding();
    }

    public int getEventType() {
        int event = -1;
        switch (state) {
        case STATE_SWITCHED:
        case STATE_COMPLETE_AT_NEXT:
            event = realReader.getEventType();
            break;
        case STATE_INIT:
            event = START_DOCUMENT;
            break;
        case STATE_COMPLETED:
            event = END_DOCUMENT;
            break;
        }
        return event;
    }

    public Location getLocation() {
        if (isDelegating()) {
            return realReader.getLocation();
        } else {
            return null;
        }
    }

    public QName getName() {
        if (isDelegating()) {
            return realReader.getName();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getLocalName() {
        if (isDelegating()) {
            return realReader.getLocalName();
        } else {
            throw new IllegalStateException();
        }
    }

    public NamespaceContext getNamespaceContext() {
        return realReader.getNamespaceContext();
    }

    public int getNamespaceCount() {
        if (isDelegating()) {
            return realReader.getNamespaceCount();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getNamespacePrefix(int i) {
        if (isDelegating()) {
            return realReader.getNamespacePrefix(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getNamespaceURI() {
        if (isDelegating()) {
            return realReader.getNamespaceURI();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getNamespaceURI(int i) {
        if (isDelegating()) {
            return realReader.getNamespaceURI(i);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getNamespaceURI(String s) {
        if (isDelegating()) {
            return realReader.getNamespaceURI(s);
        } else {
            throw new IllegalStateException();
        }
    }

    public String getPIData() {
        if (isDelegating()) {
            return realReader.getPIData();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getPITarget() {
        if (isDelegating()) {
            return realReader.getPITarget();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getPrefix() {
        if (isDelegating()) {
            return realReader.getPrefix();
        } else {
            throw new IllegalStateException();
        }
    }

    public Object getProperty(String s) throws IllegalArgumentException {
        return realReader.getProperty(s);
    }

    public String getText() {
        if (isDelegating()) {
            return realReader.getText();
        } else {
            throw new IllegalStateException();
        }
    }

    public char[] getTextCharacters() {
        if (isDelegating()) {
            return realReader.getTextCharacters();
        } else {
            throw new IllegalStateException();
        }
    }

    public int getTextCharacters(int i, char[] chars, int i1, int i2) throws XMLStreamException {
        if (isDelegating()) {
            return realReader.getTextCharacters(i, chars, i1, i2);
        } else {
            throw new IllegalStateException();
        }
    }

    public int getTextLength() {
        if (isDelegating()) {
            return realReader.getTextLength();
        } else {
            throw new IllegalStateException();
        }
    }

    public int getTextStart() {
        if (isDelegating()) {
            return realReader.getTextStart();
        } else {
            throw new IllegalStateException();
        }
    }

    public String getVersion() {
        if (isDelegating()) {
            return realReader.getVersion();
        } else {
            return null;
        }
    }

    public boolean hasName() {
        if (isDelegating()) {
            return realReader.hasName();
        } else {
            return false;
        }
    }

    public boolean hasNext() throws XMLStreamException {
        if (state == STATE_COMPLETE_AT_NEXT) {
            return true;
        } else if (state == STATE_COMPLETED) {
            return false;
        } else if (state == STATE_SWITCHED) {
            return realReader.hasNext();
        } else {
            return true;
        }
    }

    public boolean hasText() {
        if (isDelegating()) {
            return realReader.hasText();
        } else {
            return false;
        }
    }

    public boolean isAttributeSpecified(int i) {
        if (isDelegating()) {
            return realReader.isAttributeSpecified(i);
        } else {
            return false;
        }
    }

    public boolean isCharacters() {
        if (isDelegating()) {
            return realReader.isCharacters();
        } else {
            return false;
        }
    }

    public boolean isEndElement() {
        if (isDelegating()) {
            return realReader.isEndElement();
        } else {
            return false;
        }
    }

    public boolean isStandalone() {
        if (isDelegating()) {
            return realReader.isStandalone();
        } else {
            return false;
        }
    }

    public boolean isStartElement() {
        if (isDelegating()) {
            return realReader.isStartElement();
        } else {
            return false;
        }
    }

    public boolean isWhiteSpace() {
        if (isDelegating()) {
            return realReader.isWhiteSpace();
        } else {
            return false;
        }
    }
}