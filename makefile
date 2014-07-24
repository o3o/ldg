NAME = ldg.exe
VERSION = 0.1.0

ROOT_SOURCE_DIR = src
getSources = $(shell find $(ROOT_SOURCE_DIR) -name "*.cs")
SRC = $(getSources) 

SRC_TEST = $(filter-out $(ROOT_SOURCE_DIR)/app.cs, $(SRC)) 
SRC_TEST += $(wildcard tests/*.cs)
TARGET = exe

REFS = -r:System.Data.dll \
	-r:log4net.dll \
	-r:FileHelpers.dll \
	-r:itextsharp.dll \
	-r:System.Data.dll \
	-r:Mono.Data.Sqlite.dll 

REFS_TEST = $(REFS) 

DEFAULT: all

#RES_OPT = -resource:resources/$(BASE_NAME).resources,$(BASE_NAME).resources
#RES_OPT = -res:resources/mlRound32.ico,ml.ico -res:resources/ml.png,ml.png

PKG = $(wildcard $(BIN)/*.dll) $(BIN)/$(NAME) $(BIN)/$(NAME).log4net
#PKG += $(wildcard glade/*.glade)

PKG_SRC = $(PKG) README.md makefile $(SRC_TEST) tests/makefile

#################
BIN = bin
BIN_TEST = $(BIN)
CSC = dmcs

BASE_NAME = $(basename $(NAME))
CSCFLAGS += -debug -nologo -target:$(TARGET)
CSCFLAGS += -lib:$(BIN)
	CSCFLAGS += $(RES_OPT)

TEST_NAME = $(BASE_NAME)-test 
CSCFLAGS_TEST += -debug -nologo -target:exe
CSCFLAGS_TEST += -lib:$(BIN)
CSCFLAGS_TEST += -lib:$(BIN_TEST)

PETATEST_OPT =-verbose -showreport:no -htmlreport:no -dirtyexit:no

PUBLISH_DIR = $(CS_DIR)/lib/Microline/$(BASE_NAME)/$(VERSION)
PKG_PREFIX = $(BASE_NAME)-$(VERSION)
PKG_DIR = pkg/$(PKG_PREFIX)
.PHONY: all clean clobber test pkg pkgsrc publish

all: builddir $(BIN)/$(NAME)

test: builddir $(BIN_TEST)/$(TEST_NAME)
	mono $(BIN_TEST)/$(TEST_NAME) $(PETATEST_OPT)

builddir:
	@mkdir -p $(BIN)
	@mkdir -p $(BIN_TEST)

$(BIN)/$(NAME): $(SRC) | builddir
	$(CSC) $(CSCFLAGS) $(REFS) -out:$@ $^

$(BIN_TEST)/$(TEST_NAME): $(SRC_TEST) | builddir
	$(CSC) $(CSCFLAGS_TEST) $(REFS_TEST) -out:$@ $^

pkgdir:
	@mkdir -p $(PKG_DIR)

pkg: $(PKG) | pkgdir
	cp $(PKG) --parents $(PKG_DIR)
	tar -jcf $(PKG_DIR).tar.bz2 --directory pkg $(PKG_PREFIX)/
	zip $(PKG_DIR).zip $(PKG)

pkgsrc: $(PKG_SRC) | pkgdir
	tar -jcf pkg/$(BASE_NAME)-$(VERSION)-src.tar.bz2 $^

publishdir:
	@mkdir -p $(PUBLISH_DIR)

publish: publishdir
	cp -u --verbose --backup=t --preserve=all $(BIN)/$(NAME) $(PUBLISH_DIR)

tags: $(SRC)
	ctags $^

ver:
	@echo $(VERSION)

clean:
	-rm -f $(BIN)/$(NAME)
	-rm -f $(BIN)/$(TEST_NAME)

clobber:
	-rm -Rf $(BIN)

var:
	@echo SRC:$(SRC)
	@echo CSCFLAGS: $(CSCFLAGS)
	@echo SRC_TEST:$(SRC_TEST)
	@echo CSCFLAGS_TEST: $(CSCFLAGS_TEST)
	@echo VERSION: $(VERSION)

#include i18n.makefile
