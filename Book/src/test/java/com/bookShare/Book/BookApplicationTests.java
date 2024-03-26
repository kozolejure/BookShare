package com.bookShare.Book;

import static org.mockito.BDDMockito.*;
import static org.springframework.restdocs.mockmvc.RestDocumentationRequestBuilders.delete;
import static org.springframework.restdocs.mockmvc.RestDocumentationRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.hamcrest.Matchers.hasSize;
import static org.mockito.Mockito.when;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import com.bookShare.Book.Controllers.BooksController;
import com.bookShare.Book.Repository.BookRepository;
import com.bookShare.Book.Class.Book;


@WebMvcTest(BooksController.class)
public class BookApplicationTests {

	@Autowired
	private MockMvc mockMvc;

	@MockBean
	private BookRepository bookRepository;

	@Autowired
	private ObjectMapper objectMapper;

	@Test
	public void testGetAllBooks() throws Exception {
		Book book1 = new Book( "Naslov knjige 1", "Opis knjige 1", "Avtor 1", "pot/do/slike1.jpg");
		Book book2 = new Book("Naslov knjige 2", "Opis knjige 2", "Avtor 2", "pot/do/slike2.jpg");
		List<Book> allBooks = Arrays.asList(book1, book2);

		given(bookRepository.findAll()).willReturn(allBooks);


		mockMvc.perform(get("/book")
						.contentType(MediaType.APPLICATION_JSON))
				.andExpect(status().isOk())
				.andExpect(jsonPath("$", hasSize(2)));
	}


	@Test
	public void getBookById() throws Exception {
		Book book1 = new Book( "Naslov knjige 1", "Opis knjige 1", "Avtor 1", "pot/do/slike1.jpg");

		given(bookRepository.findById("1")).willReturn(Optional.of(book1));

		mockMvc.perform(get("/book/{id}", 1)
						.contentType(MediaType.APPLICATION_JSON))
				.andExpect(status().isOk());


	}


	@Test
	public void deleteBookTest() throws Exception {
		// Konfigurirajte mock, da ne naredi ničesar pri klicu deleteById (ker je void metoda)
		doNothing().when(bookRepository).deleteById(anyString());

		// Izvedite DELETE zahtevek in preverite, da je statusni koda uspešen
		mockMvc.perform(delete("/book/{id}", "1")
						.contentType(MediaType.APPLICATION_JSON))
				.andExpect(status().isOk()); // Ali .andExpect(status().isNoContent()); odvisno od vaše implementacije

		// Dodatno, lahko preverite, ali je bila metoda deleteById klicana na repozitoriju
		verify(bookRepository, times(1)).deleteById("1");
	}


	@Test
	public void createBookSimpleTest() throws Exception {
		Book book = new Book( "Naslov knjige", "Opis knjige", "Avtor knjige", "pot/do/slike.jpg");
		when(bookRepository.save(any(Book.class))).thenReturn(book);

		mockMvc.perform(post("/book")
						.contentType(MediaType.APPLICATION_JSON)
						.content(objectMapper.writeValueAsString(book)))
				.andExpect(status().isOk()); // Preprosto preverimo, ali je statusni koda OK
	}
}
