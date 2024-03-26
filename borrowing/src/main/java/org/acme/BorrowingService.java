package org.acme;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import org.eclipse.microprofile.reactive.messaging.Channel;
import org.eclipse.microprofile.reactive.messaging.Emitter;


@ApplicationScoped
public class BorrowingService {

    @Inject
    @Channel("borrowings-out")
    Emitter<String> borrowingEmitter;

    public void sendBorrowingAlert(String userId, String message) {
        String targetedMessage = String.format("User: %s, Message: %s", userId, message);
        borrowingEmitter.send(targetedMessage);
    }
}
