CXX = g++
CPPFLAGS = -g -Wall
BIN_DIR = bin

all: bin/hw3

$(BIN_DIR)/hw4: $(BIN_DIR)/stack.o $(BIN_DIR)/list.o $(BIN_DIR)
	$(CXX) $(CPPFLAGS) $(BIN_DIR)/Bag.o $(BIN_DIR)/Board.o $(BIN_DIR)/Dictionary.o $(BIN_DIR)/Player.o Scrabble.cpp -o Scrabble

$(BIN_DIR)/Bag.o: Tile.h Bag.h Bag.cpp  $(BIN_DIR)/.dirstamp
	$(CXX) $(CPPFLAGS) -c Bag.cpp -o $(BIN_DIR)/Bag.o

$(BIN_DIR)/Board.o: Board.h Board.cpp $(BIN_DIR)/.dirstamp
	$(CXX) $(CPPFLAGS) -c Board.cpp -o $(BIN_DIR)/Board.o

$(BIN_DIR)/Dictionary.o: Dictionary.h Dictionary.cpp $(BIN_DIR)/.dirstamp
	$(CXX) $(CPPFLAGS) -c Dictionary.cpp -o $(BIN_DIR)/Dictionary.o

$(BIN_DIR)/Player.o: Player.h Bag.h Tile.h Player.cpp $(BIN_DIR)/.dirstamp
	$(CXX) $(CPPFLAGS) -c Player.cpp -o $(BIN_DIR)/Player.o

$(BIN_DIR)/.dirstamp:
	mkdir -p $(BIN_DIR)
	touch $(BIN_DIR)/.dirstamp

.PHONY: cleans
clean:
	rm -rf $(BIN_DIR)
